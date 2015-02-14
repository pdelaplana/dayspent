function AppSideBar() {
    var self = this;

    self.selected = ko.observable('dashboard');

    self.groups = ko.observableArray([]);

    //
    // do init stuff here
    //

    var reportingGroupRepository = new ReportingGroupRepository();
    reportingGroupRepository.get().success(function (result) {
        $.each(result, function(i, group){
            self.groups.push(new ReportingGroupViewModel(group));
        });

    })

}