define([
    "dojo/_base/array",
    "dojo/_base/declare",

    "dijit/registry",
    "dijit/_Widget",
    "dijit/_TemplatedMixin",
    "dijit/_Container",
    "dojox/layout/TableContainer",
    "dijit/form/TextBox",
    "dijit/form/Button"
],
function (
    array,
    declare,

    registry,
    _Widget,
    _TemplatedMixin,
    _Container,
    TableContainer,
    TextBox,
    Button
) {
    return declare("tsc.editors.propertystringlist", [
            _Widget,
            _TemplatedMixin,
            _Container
    ], {
        templateString: "<div data-dojo-attach-point='container' id='container'>" +
            "</div>",
        dataCollection: [],
        postCreate: function () {
            //console.log('Post Create');

            try {
                _created = true;//record that this widget has been created

                tableContainerNode = dojo.place('<div></div>', container);//create a container for the table widget

                //container.appendChild(tableContainerNode);

                //init the data table
                _createTable(dataCollection);

                //no longer reference this, remove to avoid confusion
                dataCollection = null;

                //init add button
                var self = this;
                var addButton = new Button(
                {
                    label: "Add",
                    onClick: function () {
                        self._addRow(self.layout, "");
                    }
                });

                container.appendChild(addButton.domNode);

                isReady = true;
            } catch (e) {
                debugger;
            }
        },
        _createTable: function (data) {
            try {
                if (!_created) {
                    return;
                }

                var replacing = false;

                if (layout) {
                    layout.destroyRecursive();
                    layout = null;

                    styleNode.remove();
                    styleNode = null;

                    replacing = true;
                }

                var newContainer = dojo.place('<div class="newcontainer"></div>', tableContainerNode);

                layout = new TableContainer({
                    cols: 2,
                    showLabels: false
                },
                    newContainer
                );

                //ridiculous hack to sort out the table layout
                var layoutId = layout.domNode.id;
                styleNode = $('<style>#' + layoutId + ' table{ width:auto; }</style>');

                $('head').append(styleNode);
                //end ridiculous hack

                if (data != null) {
                    for (var i = 0; i < data.length; i++) {
                        _addRow(layout, data[i]);
                    }
                }

                if (replacing) {//Don't really know why I need  But without it, it doesn't work.
                    layout.startup();
                }

            } catch (e) {
                debugger;
            }

        },
        _addRow: function (tableLayout, keyword) {
            var textBox,
                deleteButton,
                self = this,
                onDeleteClick = function () {
                    var tableLayout = registry.getEnclosingWidget(domNode.parentNode);
                    var deleteIndex = tableLayout.getIndexOfChild(this);

                    self._removeRow(deleteIndex >> 1);
                };

            textBox = new TextBox(
                {
                    type: "text",
                    value: keyword,
                    placeholder: "Type in keyword",
                    'class': "metavalue"
                });

            deleteButton = new Button(
            {
                label: "Delete",
                onClick: onDeleteClick
            });

            tableLayout.addChild(textBox);
            tableLayout.addChild(deleteButton);

            tableLayout.layout();//update the layout rendering
        },
        _removeRow: function (rowIndex) {
            //This method rebuilds the layout table with a subset of the data

            //console.log( 'remove row: ', rowIndex );

            children = layout.getChildren();
            children.splice(rowIndex * 2, 2);

            //now extract the data
            var newData = [];

            for (var i = 0; i < children.length; i += 2) {
                newData.push(children[i].get('value'));
            }

            _createTable(newData);

        },
        _getValueAttr: function () {
            var jsonObject = [];
            var textboxCollection = dojo.query(".metavalue");

            for (var i = 0; i < textboxCollection.length; i++) {

                if (textboxCollection[i] != null) {

                    var widget = registry.byNode(textboxCollection[i]);

                    var value = widget.get("value");

                    jsonObject.push(value);
                }
            }
            return jsonObject;
        },
        _setValueAttr: function (val) {
            //console.log('_setValueAttr: ', val);

            if (_created) {
                _createTable(val);
            } else {
                dataCollection = val;//store for when the data is created
            }


        },
        isValid: function () {
            var isValid = true;
            /*var textboxCollection = dojo.query(".countryEmailTextbox");

            for (var i = 0; i < textboxCollection.length; i++) {

                if (textboxCollection[i] != null) {

                    var widget = registry.byNode(textboxCollection[i]);
                    isValid = isValid && widget.isValid();
                }
            }*/
            return isValid;
        }
    }
    );
});
