function UserProfileRepository() {
    var self = this;

    self.userName = ko.observable();
    self.fullName = ko.observable();
    self.email = ko.observable();


    self.update = function () {
        return $.ajax({
            url: '/api/profile/',
            type: 'put',
            data: {
                UserName: self.userName(),
                FullName: self.fullName(),
                Email: self.email()
            }
        });
    }

   

}