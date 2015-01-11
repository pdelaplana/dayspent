function StatusReportCategoryViewModel(data) {
    var self = this;

    self.statusReportCategoryId = ko.observable(data.statusReportCategoryId);
    self.code = ko.observable(data.code);
    self.description = ko.observable(data.description);


}