function UserSummaryViewModel(data) {
    var self = this;
    self.userId = ko.observable(data.userId);
    self.userFullName = ko.observable(data.userFullName);
    self.inProgressWork = ko.observable(data.inProgressWork);
    self.completedWork = ko.observable(data.completedWork);
    self.notStartedWork = ko.observable(data.notStartedWork);
    self.impediments = ko.observable(data.impediments);
    self.redFlags = ko.observable(data.redFlags);
    self.timeSpentInSecs = ko.observable(data.timeSpentInSecs);
    self.maxTimeAvailableInHours = ko.observable(data.maxTimeAvailableInHours);

    //
    // computed observables
    //
    self.noSubmission = ko.computed(function () {
        return ((self.inProgressWork() + self.completedWork() + self.notStartedWork() + self.impediments()) == 0);
    })

    self.timeSpent = ko.computed(function () {
        return(self.timeSpentInSecs() / (self.maxTimeAvailableInHours() * 3600)) * 100;
    })


}