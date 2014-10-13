
$.utils = {
    formatTimeSpent : function(timeSpentMins){
        function pad(str, max) {
            str = str.toString();
            return str.length < max ? pad("0" + str, max) : str;
        }

        if (timeSpentMins == null || timeSpentMins == 0)
            return '0m';

        var hour = Math.floor(timeSpentMins / 60);
        var mins = timeSpentMins % 60;
        return hour + "h " + pad(mins, 2) + "m ";
    }
}

function TimelineViewModel() {
    var self = this,
        activivitiesLoaded = 0,
        activitiesData = [];
        

    function DoDateGrouping() {
        ko.utils.arrayForEach(self.activities(), function (activity) {
            var dateGroup = null,
                thisDate = null,
                activityDate = moment(activity.startDate()).startOf('day');
                dateDiff = 0,
                i = 0;

            $.each(self.dateGroups(), function (index, value) {
                thisDate = moment(value.date()).startOf('day');
                diffDays = moment(activityDate).diff(thisDate, 'days');
                if (diffDays == 0) {
                    dateGroup = value;
                    return false;
                }
            });

            if (dateGroup == null) {
                // dategroup not found, so find dateGroup after current 
                var i = 0;
                $.each(self.dateGroups(), function (index, value) {
                    thisDate = moment(value.date()).startOf('day');
                    diffDays = moment(activityDate).diff(thisDate, 'days');
                    if (diffDays == 1) {
                        dateGroup = new DateGroup(activityDate);
                        self.dateGroups.insert(dateGroup, index + 1);
                        return false;
                    }
                })

                if (dateGroup == null) {
                    dateGroup = new DateGroup(activityDate);
                    self.dateGroups.insert(dateGroup);
                }

            };

            dateGroup.activities.push(activity);
            if (!self.activities.loading())
                dateGroup.sort();

            self.tagGroups.add(activity);
        })
    }

    function loadActivities(data) {

        //$.blockUI({ message: '<div class="fg-darken"><h2>Getting data...</h2><img src="/content/images/ajax-loader-bar-1.gif" alt="" /></div>' });
        //$('#ActivityStream').block({ message: '<div class="fg-darken"><h2>Loading...</h2><img src="/content/images/ajax-loader-bar-1.gif" alt="" /></div>' });

        activitiesData = data;
        var i = 0,
            appendItem = function (index) {
                if (index < data.length) {
                    self.activities.push(new ActivityViewModel(data[index]))
                    setTimeout(function () {
                        appendItem(index + 1);
                        if (activitiesData.length == index+1) self.activities.loading(false);
                    }, 5);
                } else {
                    self.activities.loading(false);
                }
            };
        appendItem(0);
        
    }


    self.timelineId = ko.observable();
    self.dateGroups = ko.observableArray([]).extend({ rateLimit: { timeout: 100, method: 'notifyAtFixedRate' } });
    self.activitiesHolder = ko.observableArray();
    self.activities = ko.observableArray([]);
    self.tagGroups = ko.observableArray([]);
    self.allTags = ko.observableArray([]);
    self.filteredTags = ko.observableArray([]);

    self.newActivity = new CreateActivityViewModel();

    //
    // state
    //
    self.activities.loading = ko.observable(true);

    self.activities.loaded = ko.computed(function () {
        var loadingComplete = (self.activities().length == activitiesData.length); 
        if (loadingComplete && !self.activities.loading()) {
            return true;
        }            
        else {
            return false;
        }
    }).extend({ rateLimit: { timeout: 100, method: 'notifyAtFixedRate' } })
    
    
    //
    // period filters
    //
    self.periodFilters = {
        selection: ko.observable('Loading...'),
        from: ko.observable(),
        to: ko.observable(),
        enabled: ko.observable(false),
        open: function () {
            self.periodFilters.enabled(true);
        },
        close: function () {
            self.periodFilters.enabled(false);
        },
        set: function (period) {

            // reset period covered by timeline
            self.periodFilters.enabled(false);

            var repository = new ActivityRepository();

            if (period == 'today') {
                self.periodFilters.from(moment().startOf('day'));
                self.periodFilters.to(moment().endOf('day'));
                self.periodFilters.selection('Today');

            } else if (period == 'yesterday') {
                self.periodFilters.from(moment().subtract('days', 1).startOf('day'));
                self.periodFilters.to(moment().subtract('days', 1).endOf('day'));
                self.periodFilters.selection('Yesterday');

            } else if (period == 'thisweek') {
                self.periodFilters.from(moment().startOf('week'));
                self.periodFilters.to(moment().endOf('week'));
                self.periodFilters.selection('This Week');

            } else if (period == 'thismonth') {
                self.periodFilters.from(moment().startOf('month'));
                self.periodFilters.to(moment().endOf('month'));
                self.periodFilters.selection('This Month');

            } else if (period == 'lastweek') {

                self.periodFilters.from(moment().subtract('weeks', 1).startOf('week'));
                self.periodFilters.to(moment().subtract('weeks', 1).endOf('week'));
                self.periodFilters.selection('Last Week')

            } else if (period == 'lastmonth') {

                self.periodFilters.from(moment().subtract('months', 1).startOf('month'));
                self.periodFilters.to(moment().subtract('months', 1).endOf('month'));
                self.periodFilters.selection('Last Month')

            } else if (period == 'other') {

                self.periodFilters.selection(moment(self.periodFilters.from()).format('lll') + ' to ' + moment(self.periodFilters.to()).format('lll'));

            } else {
                self.periodFilters.from(null);
                self.periodFilters.to(null);
                self.periodFilters.selection('All Time')
            }

            repository.period.from = self.periodFilters.from();
            repository.period.to = self.periodFilters.to();

            self.activities.loading(true);
            self.activities.removeAll();
            repository.get().done(function (result) {
                loadActivities(result);
                $.timeline.sidebar.filters.applySelection();    
            })
        }
    }

    //
    // tag filters
    //
    self.filterByTags = function(selectedTags) {
        if (selectedTags == null) return;
        self.filteredTags(selectedTags);

        /*
        ko.utils.arrayForEach(self.activities(), function (activity) {
            if (selectedTags.length == 0) {
                activity.visible(true);
            } else {
                activity.visible(false);
                var showable = true;
                ko.utils.arrayForEach(selectedTags, function (tag) {
                    if ($.inArray(tag, activity.tags()) == -1) {
                        showable = false;
                    }
                })
                activity.visible(showable);
            }
        })
        */
    };
    
    //
    // computed observables
    //
    self.totalTimeSpent = ko.computed(function () {
        var totalTime = 0;
        ko.utils.arrayForEach(self.activities(), function (activity) {
            if (activity.visible())
                totalTime = totalTime + activity.timeSpentMins();
        });
        return $.utils.formatTimeSpent(totalTime);
        deferEvaluation = true;
    }).extend({ rateLimit: { timeout:500, method: 'notifyWhenChangesStop' } });
   
    //
    // manual subscriptions
    //
    self.activities.subscribe(function (changes) {

        function addToGroup(dateGroup, activity){

            /*
            var i = null;
            $.each(dateGroup.activities(), function (index, value) {
                if (moment(value.startDate()).utc().isAfter(moment(activity.startDate()).utc())) {
                    i = index+1;
                }
            })
            
            if (i == null) {
                i = dateGroup.activities().length;
            }
            dateGroup.activities.insert(activity, i);
            */

            dateGroup.activities.push(activity);
            if (!self.activities.loading())
                dateGroup.sort();

            //dateGroup.activities.insertSortedElement(activity);
        }

        ko.utils.arrayForEach(changes, function (change) {

            if (change.status == 'added') {

                var activity = change.value,
                    dateGroup = null,
                    thisDate = null,
                    //activityDate = activity.dateGroup(),
                    activityDate = moment(activity.startDate()).startOf('day'),
                    dateDiff = 0,
                    i = 0;

                $.each(self.dateGroups(), function (index, value) {
                    thisDate = moment(value.date()).startOf('day');
                    //thisDate = value.date();
                    diffDays = moment(activityDate).diff(thisDate, 'days');
                    if (diffDays == 0){
                    //if (activityDate == thisDate){
                        dateGroup = value;
                        return false;
                    } 
                });
                
                if (dateGroup == null) {
                    // dategroup not found, so find dateGroup after current 
                    var i = 0;
                    $.each(self.dateGroups(), function (index, value) {
                        thisDate = moment(value.date()).startOf('day');    
                        //thisDate = value.date();
                        diffDays = moment(activityDate).diff(thisDate, 'days');
                        if (diffDays == 1) {
                        //if (activityDate>thisDate){
                            dateGroup = new DateGroup(activityDate);
                            self.dateGroups.insert(dateGroup, index);
                            return false;
                        }
                    })  
                };
                
                if (dateGroup == null) {
                    dateGroup = new DateGroup(activityDate);
                    self.dateGroups.push(dateGroup);
                }


                addToGroup(dateGroup, activity);
                self.tagGroups.add(activity);

                
            } else if (change.status == 'deleted') {
                ko.utils.arrayForEach(self.dateGroups(), function (dateGroup) {
                    var activity = dateGroup.activities.findByProperty('activityId', change.value.activityId());
                    if (activity != null) {
                        dateGroup.activities.remove(activity);
                        self.tagGroups.remove(activity);
                    }
                    
                });
            }        
        });

        /*
        if (self.activities().length == activitiesData.length) {
            $.unblockUI();
        }
        */
        
    
    }, null, 'arrayChange');
    
    self.filteredTags.subscribe(function () {

        ko.utils.arrayForEach(self.activities(), function (activity) {
            if (self.filteredTags().length == 0) {
                activity.visible(true);
            } else {
                activity.visible(false);
                var showable = true;
                ko.utils.arrayForEach(self.filteredTags(), function (tag) {
                    if ($.inArray(tag, activity.tags()) == -1) {
                        showable = false;
                    }
                })
                activity.visible(showable);
            }
        })
      

    }, null, 'arrayChange')
    
    //
    // tagGroups extenions
    //
    self.tagGroups.add = function (activity) {
        var tagGroup = self.tagGroups.findByProperty('name',activity.tagGroup());
        if (tagGroup == null) {
            tagGroup = new TagGroup(activity.tagGroup())
            self.tagGroups.push(tagGroup);
        }
        tagGroup.activities.push(activity);
    }

    self.tagGroups.remove = function (activity) {
        var tagGroup = self.tagGroups.findByProperty('name', activity.tagGroup());
        if (tagGroup != null) {
            var activity = tagGroup.activities.findByProperty('activityId', activity.activityId());
            if (activity != null) {
                tagGroup.activities.remove(activity);
            }
        }
    }



    //
    // operations
    //

    self.afterStreamRenders = function () {

       
    }

    self.get = function () {

        
        // load timeline from server
        var repository = new TimelineRepository();

        if (self.filterByTags.length > 0) {
            repository.filters.tags = self.filterByTags();
        }

        repository.get().done(function (result) {
            
            self.timelineId(result.timelineId);

            self.periodFilters.set('thisweek');


            //loadActivities(result.activities);
            
        });
        
    }

    
    // get data from server
    //self.get();

    // init
    var repository = new TagRepository();
    repository.get().done(function (result) {
        $.each(result, function (index, value) {
            self.allTags.push(new Tag(value));
        })
    })
    
    // make this timeline available globally
    $.timeline.stream = self;

}

function TagGroup(tagGroup) {
    var self = this;

    self.name = ko.observable(tagGroup);
    self.activities = ko.observableArray();
    self.timeSpent = ko.computed(function(){
        var totalTime = 0;
        ko.utils.arrayForEach(self.activities(), function (activity) {
            if (activity.visible())
                totalTime = totalTime + activity.timeSpentMins();
        })

        return $.utils.formatTimeSpent(totalTime);
    }) 

    self.add = function (activity) {


    }

    self.remove = function (activity) {


    }
}


function DateGroup(date) {
    var self = this;

    self.date = ko.observable(date);
    self.name = ko.computed(function(){
        var //startDate = moment(self.date()).startOf('day'),
            startDate = self.date(),
            currentDate = moment().startOf('day'),
            diffDays = moment(startDate).diff(currentDate, 'days');

        if (diffDays==0)
            return 'Today';
        else if (diffDays==-1)
            return 'Yesterday';
        else 
            return moment(startDate).format('ll');
    
    })
    self.activities = ko.observableArray();//.extend({ rateLimit: { timeout: 700, method: 'notifyAtFixedRate' } });

    
    // computed
    self.totalTimeSpentInMins = ko.computed(function () {
        var totalTime = 0;
        ko.utils.arrayForEach(self.activities(), function (activity) {
            if (activity.visible())
                totalTime = totalTime + activity.timeSpentMins();
        })

       return  $.utils.formatTimeSpent(totalTime);
    })

    // operations
    self.activities.getVisibleCount = function () {
        var count = 0;
        ko.utils.arrayForEach(self.activities(), function (activity) {
            if (activity.visible()) count++;
        })
        return count;
    }

    self.sort = function () {
        self.activities.sort(function (left, right) {
            if (moment(left.startDate()).isAfter(right.startDate()))
                return -1;
            else if (moment(left.startDate()).isBefore(right.startDate()))
                return 1;
            return 0;
        });

    }

}


ko.observableArray.fn.insertSortedElement = function (element) {
    var target = this;

    function compare(right,left) {

        if (moment(left.startDate()).isBefore(right.startDate()))
            return 1;
        else if (moment(left.startDate()).isAfter(right.startDate()))
            return -1;
        return 0;
    }

    function locationOf(element, array, end, start) {
        if (array.length === 0)
            return 1;

        start = start || 0;
        end = end || array.length;
        var pivot = (start + end) >> 1;  // should be faster than the above calculation

        var c = compare(element, array[pivot]);
        if (end - start <= 1) return c == -1 ? pivot - 1 : pivot;

        switch (c) {
            case -1: return locationOf(element, array, start, pivot);
            case 0: return pivot;
            case 1: return locationOf(element, array, pivot, end);
        };
    };

    target.splice(locationOf(element, target)+1, 0, element);
    return target;


}