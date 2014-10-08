function CreateActivityViewModel() {
    var self = this;

    self.description = ko.observable();
    self.startDate = ko.observable(moment());
    self.timeSpent = ko.observable();
    self.timeSpentInMins = ko.observable(0);
    self.tags = ko.observableArray();

    self.adding = ko.observable(false);

    self.create = function () {
        var repository = new ActivityRepository();
        repository.timelineId = $.timeline.stream.timelineId();
        repository.description = self.description();
        repository.startDate = moment(self.startDate()).toJSON();
        repository.timeSpent = self.timeSpent();
        repository.tags = self.tags();
        repository.create().done(function (result) {

            // check if the new activity falls within period filters
            if (moment(result.data.startDate).isAfter(moment($.timeline.stream.periodFilters.from()))
                && moment(result.data.startDate).isBefore(moment($.timeline.stream.periodFilters.to()))){

                $.timeline.stream.activities.insert(new ActivityViewModel(result.data), 0);
            }

            self.description('');
            self.startDate(moment().toDate());
            self.timeSpent('');
            self.timeSpentInMins(0);
            self.tags([]);

            $.Notify.show("A new time entry has been added.")

        })

    }


    self.cancel = function () {
        self.description('');
        self.startDate(moment().toDate());
        self.timeSpent('');
        self.timeSpentInMins(0);
        self.tags('');
        self.adding(false);

    }
}