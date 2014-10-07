function Router(contentContainer, rootPath) {

    var sammy = Sammy(contentContainer, function () {
        var self = this;

        // helpers
        self.helpers({
            loadLocation: function (url, initialize) {
                $.blockUI({ message: '<div class="fg-darken"><h2>Loading...</h2><img src="/content/images/ajax-loader-bar-1.gif" alt="" /></div>' });
                return this.load(url, { cache: false })
                            .swap(function () {
                                initialize(self.element_selector);
                                $.unblockUI();
                            });
            },
            loadTargetLocation: function (url, initialize, target) {
                return this.load(url, { cache: false })
                            .then(function (content) {
                                var $target = sammy.$element().find(target);
                                $target.html(content)
                                initialize();
                            });
            }
        });

        self.swap = function (content, callback) {
            var context = this;
            
            context.$element().html(content);
            if (callback) {
                callback.apply();
            }

            
            /*
            context.$element().fadeOut('fast', function () {
                context.$element().html(content);
                if (callback) {
                    callback.apply();
                }
                context.$element().fadeIn('fast', function () {});
            });

	        context.$element().html(content);
	        if (callback) {
	            callback.apply();
	        }
	        legis.app.ui.unblock();

            

            */

            
        };

        self.error = function (message, original_error) {

            alert(message);
        }

        self.get('app/#/', function (context) {
            context.redirect('#/boards');
        });

        self.notFound = function () {
            alert('Not found');
        }

    });

    return {

        go: function (route) {
            sammy.setLocation(route);
        },

        registerRoute: function (route, found, before) {
            
            sammy.get(route, found);

            if (before != undefined) {
                sammy.before(route, before);
            }

        },

        registerPostRoute: function (route, found) {
            sammy.post(route, found);
        },

        
        run: function (path) {
            sammy.run(path);
        }

    }
}