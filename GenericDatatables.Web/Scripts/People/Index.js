var personDatatable;

$(document).ready(function () {
    bindButton($(".btn-add"));
    setupDatatable();
});

function bindButton(button) {
    button.click(function (event) {
        event.preventDefault();
        var url = $(this).attr('href');
        $.ajax({
            url: url,
            type: 'GET',
            success: function (result) {
                $("#modaldiv").html(result);
                $("#modaldiv").modal();
                setupPersonForm();
            }
        });
    });    
}

function setupDatatable() {
    personDatatable = $("#personDatatable").dataTable({
        sDom: 'C<"clear">lfrtip',
        //sScrollY: "200px",
        bProcessing: true,
        bServerSide: true,
        sAjaxSource: $("#urls-datatable").text(),
        fnServerParams: function (aoData) {
            aoData.push({ name: "datatableId", value: "personDatatable" });
        },
        aoColumns: [
                    { mDataProp: 'Id' },
                    { mDataProp: 'Name' },
                    { mDataProp: 'Birthday' },
                    { mDataProp: 'Address' },
                    { mDataProp: 'Time' },
                    { mDataProp: 'Actions', bSearchable: false, bSortable: false }
            ],
        fnDrawCallback: function () {
            bindButton($(".btn-edit"));
        }
    });

    $("tfoot input").keyup(function () {
        /* Filter on the column (the index) of this element */
        personDatatable.fnFilter(this.value, $("tfoot input").index(this));
    });

    return personDatatable;
}


function setupPersonForm() {
    $("#input[form='person-form']").click(function(event) {
        event.preventDefault();
        $("#person-form").submit();
    });

    $("#Birthday").datepicker({
        showAnim: 'slide'
    });
    $("#Birthday").inputmask('dd/mm/yyyy');

    $("#Time").inputmask('hh:mm');

    $('form').submit(function () {
        $.ajax({
            url: this.action,
            type: this.method,
            data: $(this).serialize(),
            success: function (result) {
                if (result.success) {
                    $("#modaldiv").modal('hide');
                    personDatatable.fnReloadAjax();
                } else {
                    $("#modaldiv").html(result);
                    setupPersonForm();
                }
            }
        });
        return false;
    });
}