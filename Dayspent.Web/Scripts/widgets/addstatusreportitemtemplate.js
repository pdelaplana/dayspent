function AddStatusReportItemTemplate() {
    var self = this;
    self.activated = ko.observable(false);
    self.statusReportId = ko.observable();
    self.statusReportCategoryId = ko.observable();
    self.category = ko.observable();
    self.description = ko.observable();
    self.tags = ko.observableArray();
    self.activate = function(){
        self.activated(true);
    }
    self.save = function () {
        var repository = new StatusReportItemRepository();
        repository.statusReportId = self.statusReportId();
        repository.statusReportCategoryId = self.statusReportCategoryId();
        repository.description = self.description();
        return repository.create().success(function (result) {
            var item = new StatusReportItemViewModel(result.data)
            collection.push(item);
            //viewModel.reportItems.push(item);
            inputControlContainer.remove();
            $(element).show();
        });

    }
    self.cancel = function () {
        self.activated(false);
    }

}