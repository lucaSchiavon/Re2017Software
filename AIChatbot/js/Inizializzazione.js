var CONFIG = (function () {
    var private = {
        'API_URL': 'http://re2017-app.azurewebsites.net'
    };

    return {
        get: function (name) { return private[name]; }
    };
})();

