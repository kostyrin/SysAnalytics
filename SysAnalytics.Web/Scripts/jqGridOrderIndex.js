$(document).ready(function (result) {
    var obj = jQuery.parseJSON(result);
    colM = obj.DocumentElement.colModel;

    $('#gridOrder').jqGrid({
        //colNames: ['OrderId', 'StudyLevel', 'AssignDate'],
        colModel: colM,
        pager: jQuery('#pager'),
        sortname: 'StudyLevel',
        rowNum: 10,
        rowList: [10, 20, 50],
        sortorder: "asc",
        /* width: 600,
         height: 250,*/
        datatype: 'json',
        caption: 'Result of scanning',
        viewrecords: true,
        mtype: 'GET',
        jsonReader: {
            root: "rows",
            page: "page",
            total: "total",
            records: "records",
            repeatitems: false,
            userdata: "userdata"
        },
        //or ondblClickRow
        onSelectRow: function (id) {
            var row = $('#gridOrder').getRowData(id);
            //GetInfo(row['Computer_Name'], row['Computer_IP']);
        },
        url: "/Order/GetOrders"
    }).navGrid('#pager', { view: false, del: false, add: false, edit: false },
       {}, // default settings for edit
       {}, // default settings for add
       {}, // delete instead that del:false we need this
       { closeOnEscape: true, multipleSearch: true, closeAfterSearch: true }, // search options
       {} /* view parameters*/
     );

});