define([
// dojo
        "dojo/_base/lang",
        "dojo/Deferred",
        "epi/dependency"
],
    function (
// dojo
        lang,
        Deferred,
        dependency
    ) {
        function resolveContentData(contentlink, callback) {
            if (!contentlink) {
                return null;
            }

            var registry = dependency.resolve("epi.storeregistry");
            var store = registry.get("epi.cms.content.light");

            var contentData;
            dojo.when(store.get(contentlink),
                function (returnValue) {
                    contentData = returnValue;
                    callback(contentData);
                });
            return contentData;
        };

        var images = {};

        var extendedFormatters = {
            imageFormatter: function (value) {
                if (!value) {
                    return "-";
                }
                if (!images[value]) {
                    return value;
                }
                return "<img style='max-height: 50px;' src='" + images[value] + "'/>";
            },

            resolveImageMapping: function (contentLink) {
                var def = new Deferred();
                resolveContentData(contentLink,
                    function (contentData) {
                        images[contentLink] = contentData.previewUrl;
                        def.resolve();
                    });
                return def.promise;
            },

            setImageMapping: function (contentLink, imageUrl) {
                images[contentLink] = imageUrl;
            }
        };
        return extendedFormatters;
    });
