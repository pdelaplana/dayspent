﻿function Application(config) {

    config = $.extend({
        appTitle : 'My Application',
        appContainer: 'application-container'
    }, config);

    function ViewsArray() {
        this.add = function (name, view) {
            this[name] = view;
        }
    }
    ViewsArray.prototype = [];
    
    var ui = new UI(),
        router = new Router(config.appContainer),
        views = new ViewsArray();

    ui.appTitle(config.appTitle);

    return {

        user: {
            userId : '',
            userName: '',
            fullName: ''
        },

        ui: ui,

        router: router,
    
        views: views,

        cache: new Cache(),

        start: function (path) {

            router.run(path);
        }
    }
    

}