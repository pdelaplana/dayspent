function ReportingGroupRepository() {
    var self = this;

    self.get = function () {
        
        return $.ajax({
            url: '/api/groups',
            type: 'get'
        })

    }

}