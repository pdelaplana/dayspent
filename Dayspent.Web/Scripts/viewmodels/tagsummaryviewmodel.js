function TagSummaryViewModel() {
    var self = this;

    self.startDate = ko.observable(moment());
    self.endDate = ko.observable(moment());

    self.statusReportItems = ko.observableArray();//.extend({ rateLimit: { timeout: 1600, method: "notifyWhenChangesStop" } });

    self.persons = ko.observableArray([]);
    //self.tags = ko.observableArray([]);

    //
    // state observables
    //
    self.isUpdating = ko.observable(false);


    //
    // computed observables
    //
    self.totalTimeSpentInSecs = ko.computed(function () {
        var totalTime = 0;

        $.each(self.persons(), function (i, person) {
            $.each(person.tags(), function (i, tagDetail) {
                if (tagDetail.selected()) {
                    totalTime += tagDetail.timeSpentInSecs();
                }
            })
        })
        return totalTime;
    })//.extend({ rateLimit: { timeout: 1600, method: "notifyWhenChangesStop" } })

    
    self.tagFilters = ko.computed(function () {
        var tags = [],
            tagFilters = [];
        $.each(self.statusReportItems(), function (i, item) {
            $.each(item.tags(), function (j, tag) {
                tags.push(tag);
            })
        })
        tags = ko.utils.arrayGetDistinctValues(tags).sort();
        $.each(tags, function (i, tag) {
            tagFilters.push(new TagFilter(tag));
        })
        // handle items with no tags
        var items = [];
        $.each(self.statusReportItems(), function (j, item) {

            if (item.tags().length == 0) {
                tagFilters.push(new TagFilter('-'));
                return false;
            }
        })
        
        return tagFilters;
    })

    /*
    self.tagsSorted = ko.computed(function () {
        self.tags();
        return self.tags.sort(function (a, b) {
            return (a.timeSpentInSecs() > b.timeSpentInSecs()) ? -1 : (a.timeSpentInSecs() < b.timeSpentInSecs()) ? 1 : 0;
        })
    })
    */
    
    //
    // operations
    //
    self.toggleFilterSelection = function (tagFilter) {
        tagFilter.selected(!tagFilter.selected());

        var tags = [];
        $.each(self.tagFilters(), function (i, tagFilter) {
            if (tagFilter.selected()) tags.push(tagFilter.tag());
        })

        $.each(self.persons(), function (i, person) {
            $.each(person.tags(), function (i, tag) {
                if ($.inArray(tag.tag(), tags) == -1) {
                    tag.selected(false);
                } else {
                    tag.selected(true);
                }
            })
        })
    }

    self.loadData = function () {
        self.statusReportItems([]);
        $.ajax({
            url: '/api/dashboard/tagsummary',
            type:'post',
            dataType: 'json',
            data: {
                StartDate: moment.utc(self.startDate()).toJSON(),
                EndDate: moment.utc(self.endDate()).toJSON()
            },
            success: function (result) {

                function sumUpTime(items) {
                    var total = 0;
                    $.each(items, function (i, item) {
                        total += item.timeSpentInSecs();
                    })
                    return total;
                }
                self.isUpdating(true);
                self.statusReportItems([]);
                //self.tags([]);
                self.persons([]);

                $.each(result, function (index, item) {
                    self.statusReportItems.push(new StatusReportItemViewModel(item));
                });

                // get distinct array of persons
                var persons = [];
                $.each(self.statusReportItems(), function(i, item){
                    persons.push(item.reportingUserId())
                })
                persons = ko.utils.arrayGetDistinctValues(persons);

                // get reports per person
                var reportItems = [];
                $.each(persons, function (i, person) {

                    var personDetail = new PersonDetail();
                    personDetail.userId(person);

                    // filter report itemns by this user
                    reportItems = self.statusReportItems.filterByProperty('reportingUserId', person);

                    // get the full name 
                    personDetail.userFullName(reportItems()[0].reportingUserFullName());


                    var tags = [];
                    $.each(reportItems(), function (i, item) {
                        $.each(item.tags(), function (j, tag) {
                            tags.push(tag);
                        })
                    })
                    tags = ko.utils.arrayGetDistinctValues(tags).sort();

                    $.each(tags, function (i, tag) {
                        var items = [];
                        $.each(reportItems(), function (j, item) {
                            if ($.inArray(tag, item.tags()) > -1) {
                                items.push(item);
                            }
                        })
                        var tagDetail = new TagDetail();
                        tagDetail.tag(tag);
                        tagDetail.timeSpentInSecs(sumUpTime(items));
                        tagDetail.selected(true);
                        personDetail.tags.push(tagDetail);
                    });

                    // handle items with no tags
                    var items = [];
                    $.each(reportItems(), function (j, item) {

                        if (item.tags().length == 0) {
                            items.push(item);
                        }
                    })
                    if (items.length > 0) {
                        var tagDetail = new TagDetail();
                        tagDetail.tag('-');
                        tagDetail.timeSpentInSecs(sumUpTime(items));
                        personDetail.tags.push(tagDetail);
                    }
                    self.persons.push(personDetail);
                })

                /*
                var tags = [];
                $.each(self.statusReportItems(), function (index, item) {
                    $.each(item.tags(), function (i, tag) {
                        tags.push(tag);
                    })
                })
                tags = ko.utils.arrayGetDistinctValues(tags).sort();

               
                $.each(tags, function (i, tag) {
                    var items = [];
                    $.each(self.statusReportItems(), function (j, item) {
                        if ($.inArray(tag, item.tags()) > -1) {
                            items.push(item);
                        }
                        // handle items with no tags
                    })
                    var tagDetail = new TagDetail();
                    tagDetail.tag(tag);
                    tagDetail.timeSpentInSecs(sumUpTime(items));
                    tagDetail.selected(true);
                    self.tags.push(tagDetail);
                });

                // handle items with no tags
                var items = [];
                $.each(self.statusReportItems(), function (j, item) {

                    if (item.tags().length == 0) {
                        items.push(item);
                    }
                })
                if (items.length > 0) {
                    var tagDetail = new TagDetail();
                    tagDetail.tag('-');
                    tagDetail.timeSpentInSecs(sumUpTime(items));
                    self.tags.push(tagDetail);
                }
                */
                self.isUpdating(false);

            }
        });
    }

}

function PersonDetail() {
    var self = this;
    self.userId = ko.observable();
    self.userFullName = ko.observable();

    self.tags = ko.observableArray();
    self.selected = ko.observable(true);
    self.timeSpentInSecs = ko.computed(function () {
        var totalTime = 0;
        $.each(self.tags(), function (i, tag) {
            if (tag.selected()) totalTime += tag.timeSpentInSecs();
        })
        return totalTime;
    })

}

function TagDetail() {
    var self = this;
    self.tag = ko.observable();
    self.timeSpentInSecs = ko.observable();
    self.selected = ko.observable(true);

    self.toggleSelection = function () {
        self.selected(!self.selected());
    }

}

function TagFilter(tag) {
    var self = this;
    self.tag = ko.observable(tag);
    self.timeSpentInSecs = ko.observable();
    self.selected = ko.observable(true);

    self.toggleSelection = function () {
        self.selected(!self.selected());
    }


}