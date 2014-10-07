function UserAvatarRepository() {
    var self = this;

    self.userId = null;
    self.userName = null;

    self.file = null;
    self.fileName = null;
    self.fileContentType = null;
    self.fileUrl = null;
    self.fileContents = null;

    
    self.upload = function () {
        var formData = new FormData();
        formData.append('Contents', self.file);
        return $.ajax({
            url: '/api/avatar/'+self.userId,
            type: 'put',
            data: formData,
            contentType: false,
            cache: false,
            processData: false
        });
    }

}