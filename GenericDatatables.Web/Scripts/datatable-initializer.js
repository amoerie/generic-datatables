$(function() {
    var $table = $("#GymMembersTable");
    var settings = getDefaultRemoteDatatableSettings($table);
    var datatable = $table.dataTable(settings);
    
    var $filters = $table.find('thead tr.datatable-column-filters');
    $filters.on('change keyup', "input", search);
    $filters.on('change', "select", search);

    function search() {
        var jqXHR = datatable.fnSettings().jqXHR;
        if (jqXHR != null) {
            jqXHR.abort();
        }
        datatable.fnFilter($(this).val(), $filters.find("td").index($(this).parents('td').first()));
    }
});


function isDefined(value) {
    /*-------------------------------
        isDefined
    ---------------------------------

    Returns true if a value is defined

    */
    return typeof value != 'undefined';
}

function getValueOrDefault(value, valueIfUndefined) {
    /*-------------------------------
        getValueOrDefault
    ---------------------------------

    Returns value or valueIfUndefined if value was not defined

    */
    if (isDefined(value))
        return value;
    else
        return valueIfUndefined;
}

function getDefaultRemoteDatatableSettings($datatable) {
    /*-------------------------------
        getDefaultRemoteDatatableSettings (Alex)
    ---------------------------------
    
        parameters:
            datatable
                The jQuery element of the table
        summary
            Gets the default remote settings
    
    */
    var $columnHeaders = $datatable.find('thead tr.datatable-column-headers th');
    var dataProperties = _.map($columnHeaders, getDatatableHeaderColumnData);

    return {
        "aoColumns": dataProperties,
        "bAutoWidth": false,
        "bProcessing": true,
        "bStateSave": true,
        "bServerSide": true,
        "fnDrawCallback": function () { },
        "fnInitComplete": function () {
            // load search filters from state into input fields
            var oSettings = $datatable.dataTable().fnSettings();
            for (var i = 0 ; i < oSettings.aoPreSearchCols.length ; i++) {
                if (oSettings.aoPreSearchCols[i].sSearch.length > 0) {
                    var cachedValue = oSettings.aoPreSearchCols[i].sSearch;
                    var $propertyColumn = $($("thead tr.datatable-column-filters td")[i]);
                    $propertyColumn.find(".datatable-column-filter").val(cachedValue);
                }
            }
        },
        "fnServerData": function (sSource, aoData, fnCallback, oSettings) {
            oSettings.jqXHR = $.ajax({
                "dataType": 'json',
                "type": "POST",
                "url": sSource,
                "data": aoData,
                "success": fnCallback
            });
        },
        "fnServerParams": function (aoData) {
            aoData.push({ name: "sDatatableId", value: $datatable.attr('id') });
        },
        "sAjaxSource": $datatable.attr('data-url')
    };
}

function getDefaultLocalDatatableSettings(datatable) {
    /*-------------------------------
        getDefaultLocalDatatableSettings (Alex)
    ---------------------------------
    
        parameters:
            datatable
        summary
            Gets the default local settings
    
    */
    var $columnHeaders = datatable.find('thead tr.datatable-column-headers th');
    var dataProperties = _.map($columnHeaders, getDatatableHeaderColumnData);

    return {
        "aoColumns": dataProperties,
        "bAutoWidth": false,
        "bProcessing": false,
        "bStateSave": true,
        "bServerSide": false,
        "fnDrawCallback": function () { },
        "fnInitComplete": function () {
            var oSettings = datatable.dataTable().fnSettings();
            for (var i = 0 ; i < oSettings.aoPreSearchCols.length ; i++) {
                if (oSettings.aoPreSearchCols[i].sSearch.length > 0) {
                    var cachedValue = oSettings.aoPreSearchCols[i].sSearch;
                    var $propertyColumn = $($("thead tr.datatable-column-filters td")[i]);
                    $propertyColumn.find("input.datatable-column-filter").val(cachedValue);
                    $propertyColumn.find("select.datatable-column-filter").select2("val", cachedValue);
                }
            }
        }
    };
}

function getDatatableHeaderColumnData(datatableHeader) {
    // each column has specific options. These are written to data-... attributes on the <th> tag
    // this method reads those data-... properties and converts them into an mDataProp object that can be used with the datatables API.
    var $datatableHeader = $(datatableHeader);
    var property = $datatableHeader.data('property');
    var sSearchable = $datatableHeader.data('searchable');
    var bSortable = $datatableHeader.data('sortable');
    var sWidth = $datatableHeader.data('width');
    var bVisible = $datatableHeader.data('visible');
    var sClass = $datatableHeader.data('class');
    var sDefaultContent = $datatableHeader.data('default-content');

    var columnData = {
        mDataProp: property
    };

    if (isDefined(sWidth) && sWidth != null && sWidth.length > 0)
        columnData.sWidth = sWidth;

    if (isDefined(sSearchable) && sSearchable != null)
        columnData.bSearchable = sSearchable;

    if (isDefined(bSortable) && bSortable != null)
        columnData.bSortable = bSortable;

    if (isDefined(bVisible) && bVisible != null)
        columnData.bVisible = bVisible;

    if (isDefined(sClass) && sClass != null && sClass.length > 0)
        columnData.sClass = sClass;

    if (isDefined(sDefaultContent) && sDefaultContent != null && sDefaultContent.length > 0)
        columnData.sDefaultContent = sDefaultContent;

    return columnData;
}