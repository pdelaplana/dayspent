function TagRepository() {
    var self = this;

    self.get = function () {
        return $.ajax({
            url: '/api/tags/all',
            type: 'get'
        });


    }
}