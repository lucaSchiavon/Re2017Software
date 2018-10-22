var CONFIG = (function () {
    var private = {
        'API_URL': 'http://2.235.241.7:8080'
    };

    return {
        get: function (name) { return private[name]; }
    };
})();

