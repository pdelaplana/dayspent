function PeriodFilters() {
    var self = this;

    self.selection = ko.observable('This Week');
    self.from = ko.observable();
    self.to = ko.observable();
    self.enabled = ko.observable(false);
    self.open = function () {
        self.periodFilters.enabled(true);
    };
    self.close = function () {
        self.periodFilters.enabled(false);
    };
    self.set = function (period) {

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

        self.activities.removeAll();
        repository.get().done(function (result) {
            loadActivities(result);
            $.timeline.sidebar.filters.applySelection();    
        })
    }


}