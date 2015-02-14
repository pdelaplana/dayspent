function ReportingGroupViewModel(data) {
    var self = this;
    self.reportingGroupId = ko.observable(data.reportingGroupId);
    self.name = ko.observable(data.name);

}