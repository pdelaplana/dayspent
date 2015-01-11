function StatusReportItemRepository() {
    var self = this;

    self.statusReportItemId = null;
    self.statusReportId = null;
    self.statusReportCategoryId = null;
    self.description = null;
    self.timeSpent = null;
    self.tags = null;


    self.create = function () {
        return $.ajax({
            url: '/api/report/'+self.statusReportId+'/items',
            type: 'post',
            data: {
                StatusReportId: self.statusReportId,
                StatusReportCategoryId: self.statusReportCategoryId,
                Description: self.description,
                Tags:self.tags
            }
        })
    }

    self.update = function () {
        return $.ajax({
            url: '/api/report/' + self.statusReportId + '/items/' + self.statusReportItemId,
            type: 'put',
            data: {
                StatusReportItemId: self.statusReportItemId,
                Description: self.description
            }
        })

    }

    self.remove = function () {
        return $.ajax({
            url: '/api/report/' + self.statusReportId + '/items/'+self.statusReportItemId,
            type: 'delete'
        })
    }

    self.addTimeSpent = function () {
        return $.ajax({
            url: '/api/report/' + self.statusReportId + '/items/' + self.statusReportItemId + '/timespent',
            type: 'put',
            data: {
                StatusReportItemId: self.statusReportItemId,
                TimeSpent: self.timeSpent
            }
        })
    }


}