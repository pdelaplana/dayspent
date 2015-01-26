function StatusReportRepository() {
    var self = this;

    self.statusReportId = null;
    self.reportDate = null;
    self.submittedDate = null;

    self.statusReportCategoryId = null;
    self.statusReportItemIds = null;

    self.get = function () {
        return $.ajax({
            url: '/api/reports/'+app.user.userId,
            dataType: 'json',
            type: 'get'
        })
    }

    self.update = function () {
        return $.ajax({
            url: '/api/reports/' + self.statusReportId,
            type: 'put',
            data: {
                StatusReportId: self.statusReportId,
                ReportDate: moment(self.reportDate).utc().toJSON()
            }
        });
    }

    self.submit = function () {
        return $.ajax({
            url: '/api/reports/' + self.statusReportId+'/submit',
            type: 'put',
            data: {
                StatusReportId: self.statusReportId,
                SubmittedDate: self.submittedDate.toJSON()
            }
        });

    }

    self.reposition = function () {
        return $.ajax({
            url: '/api/reports/' + self.statusReportId + '/reposition',
            type: 'put',
            data: {
                StatusReportId: self.statusReportId,
                StatusReportCategoryId: self.statusReportCategoryId,
                StatusReportItemIds: self.statusReportItemIds
            }
        });

    }

    self.remove = function () {
        return $.ajax({
            url: '/api/reports/' + self.statusReportId,
            type: 'delete'
        });
    }

}