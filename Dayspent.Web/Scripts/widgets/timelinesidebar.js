
function TimelineSidebar() {
    var self = this;

    //
    // observables
    //
    self.viewing = ko.observable('ActivityStream');
    self.viewDashboard = function (data, event) {
        self.viewing('Dashboard');
        $.ajax({
            url: '/timeline/dashboard',
            type: 'get',
            dataType: 'html',
            success: function (result) {
                var contentRegion = $('.page-region-content');
                contentRegion.html(result);
                ko.cleanNode(contentRegion.get(0));
                ko.applyBindings(new DashboardViewModel, contentRegion.get(0));
                $('[data-role=dropdown]').dropdown();
            }
        })
    }
    self.viewActivityStream = function (data, event) {
        self.viewing('ActivityStream');
        $.ajax({
            url: '/timeline/activitystream',
            type: 'get',
            dataType: 'html',
            success: function (result) {
                var contentRegion = $('.page-region-content');
                contentRegion.html(result);
                ko.cleanNode(contentRegion.get(0));
                var timelineViewModel = new TimelineViewModel();
                ko.applyBindings(timelineViewModel, contentRegion.get(0));
                timelineViewModel.get();
                $('[data-role=dropdown]').dropdown();
            }
        })
    }
    self.viewReports = function (data, event) {
        self.viewing('Reports');
        $.ajax({
            url: '/timeline/reports',
            type: 'get',
            dataType: 'html',
            success: function (result) {
                var contentRegion = $('.page-region-content');
                contentRegion.html(result);
                ko.cleanNode(contentRegion.get(0));
                ko.applyBindings(new ReportViewModel(), contentRegion.get(0));
            }
        })
    }


    self.tags = ko.observableArray();
    self.filters = {
        tags: ko.observableArray(),
        applySelection: function(){
            $.timeline.stream.filterByTags(self.filters.tags());
        },
        clearAllTags: function () {
            self.filters.tags.removeAll();
            self.filters.applySelection();
        }
    }
    

   

    // init
    var repository = new TagRepository();
    repository.get().done(function (result) {
        $.each(result, function (index, value) {
            self.tags.push(new Tag(value));
        })
    })

    $.timeline.sidebar = self;


}

function Tag(name) {
    var self = this;

    self.name = ko.observable(name);
    self.selected = ko.computed(function () {
        var result = $.inArray(self.name(), $.timeline.sidebar.filters.tags()) > -1;
        return result;
    });

    
    self.addOrRemoveFromFilters = function () {
        if (self.selected()) {
            $.timeline.sidebar.filters.tags.remove(self.name());
            $.timeline.sidebar.filters.applySelection();
        } else {
            $.timeline.sidebar.filters.tags.push(self.name());
            $.timeline.sidebar.filters.applySelection();
            
        }

    }
}