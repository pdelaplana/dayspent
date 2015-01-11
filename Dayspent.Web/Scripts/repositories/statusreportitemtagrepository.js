function StatusReportItemTagRepository() {
    var self = this;
    self.name = null;
    self.statusReportId = null;
    self.statusReportItemId = null;

    self.create = function () {
        return $.ajax({
            url: '/api/report/'+self.statusReportId+'/items/'+self.statusReportItemId+'/tags',
            type: 'post',
            data: {
                StatusReportItemId: self.statusReportItemId,
                TagName: self.name
            }
        });
    }

    self.remove = function () {
        return $.ajax({
            url: '/api/report/'+self.statusReportId+'/items/'+self.statusReportItemId+'/tags/'+self.name,
            type: 'delete'
        });

    }
}