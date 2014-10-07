function TimelineRepository() {
    var self = this;

    self.filters = {
        tags : null
    }

    self.get = function () {
        var getData = {};
        getData.tags = self.filters.tags || null;
        
        return $.ajax({
            url: '/api/timeline',
            type: 'get',
            data: getData
        })
    }
}