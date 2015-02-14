function StatusReportItemViewModel(data) {
    var self = this;

    self.statusReportItemId = ko.observable(data.statusReportItemId);
    self.statusReportId = ko.observable(data.statusReportId);

    self.reportDate = ko.observable(data.reportDate);

    self.reportingUserId = ko.observable(data.reportingUserId);
    self.reportingUserFullName = ko.observable(data.reportingUserFullName);

    self.statusReportCategoryId = ko.observable(data.statusReportCategoryId);
    self.statusReportCategoryDescription = ko.observable(data.statusReportCategoryDescription);
    self.statusReportCategoryCode = ko.observable(data.statusReportCategoryCode);

    self.description = ko.observable(data.description);

    self.sequence = ko.observable(data.sequence);
    
    self.timeSpent = ko.observable();
    self.timeSpentInSecs = ko.observable(data.timeSpentInSecs);

    self.tags = ko.observableArray(data.tags);
    self.tags.enabled = ko.observable(false);

    //
    // computed observables
    //
    self.timeSpentFormatted = ko.computed(function () {

        function pad(str, max) {
            str = str.toString();
            return str.length < max ? pad("0" + str, max) : str;
        }

        if (self.timeSpentInSecs() == null || self.timeSpentInSecs() < 60)
            return 'add time';
        var timeSpentMins = Math.floor(self.timeSpentInSecs() / 60);
        var hour = Math.floor(timeSpentMins / 60);
        var mins = timeSpentMins % 60;
        return hour + "h " + pad(mins, 2) + "m ";

    })

    self.tagGroup = ko.computed(function () {
        var result = "";
        $.each(self.tags().sort(), function (index,tag) {
            if (result != "") result+=", ";
            result += tag;
        });
        return result == '' ? 'untagged': result;;
    })

    self.reportingUserShortName = ko.computed(function () {
        return self.reportingUserFullName().split(' ')[0];
    })

    //
    // operations
    //
    self.update = function () {
        var repository = new StatusReportItemRepository();
        repository.statusReportItemId = self.statusReportItemId();
        repository.description = self.description();
        return repository.update();
    }

    self.remove = function () {
        var repository = new StatusReportItemRepository();
        repository.statusReportItemId = self.statusReportItemId();
        return repository.remove();
    }

    self.incrementTimeSpent = function (item, event) {
        var incrementBy = $(event.target).data('param'),
            repository = new StatusReportItemRepository();

        if (incrementBy == null) incrementBy = 15;

        repository.statusReportItemId = self.statusReportItemId();
        repository.timeSpent = '+' + incrementBy;
        return repository.addTimeSpent().success(function (result) {
            self.timeSpentInSecs(result.data.timeSpentInSecs);
        });
    }

    self.addTimeSpent = function () {
        var repository = new StatusReportItemRepository();
        repository.statusReportItemId = self.statusReportItemId();
        repository.timeSpent = self.timeSpent();
        return repository.addTimeSpent();
    }

    self.addTag = function (tagName) {
        var repository = new StatusReportItemTagRepository();
        repository.statusReportId = self.statusReportId();
        repository.statusReportItemId = self.statusReportItemId();
        repository.name = tagName;
        repository.create().done(function (result) {
            self.tags.push(result.data.tagName);
        })
    }

    self.removeTag = function (tagName) {
        var repository = new StatusReportItemTagRepository();
        repository.statusReportId = self.statusReportId();
        repository.statusReportItemId = self.statusReportItemId();
        repository.name = tagName;
        repository.remove().done(function (result) {
            self.tags.remove(result.data.tagName);
        })
    }



}