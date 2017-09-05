define([
    "dojo/_base/array",
    "dojo/_base/declare",
    "dojo/_base/lang",
    "epi-cms/contentediting/editors/CollectionEditor",
    "tsc/editors/extendedFormatters"
],
function (
    array,
    declare,
    lang,
    CollectionEditor,
    extendedFormaters
) {
    return declare([CollectionEditor], {
        _getGridDefinition: function () {
            var result = inherited(arguments);
            if (mappedImages !== undefined) {
                for (var i = 0; i < mappedImages.length; i++) {
                    extendedFormaters.setImageMapping(mappedImages[i].id, mappedImages[i].imageUrl);
                }
            }
            result.image.formatter = extendedFormaters.imageFormatter;
            return result;
        },

        onExecuteDialog: function () {
            var item = _itemEditor.get("value");
            var resolveImageMapping = extendedFormaters.resolveImageMapping(item.image);

            resolveImageMapping.then(lang.hitch(this, function () {
                if (_editingItemIndex !== undefined) {
                    model.saveItem(item, _editingItemIndex);
                } else {
                    model.addItem(item);
                }
            }));
        }
    });
});
