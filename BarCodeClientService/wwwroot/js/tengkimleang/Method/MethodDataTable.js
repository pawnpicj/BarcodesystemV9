class DataTableMethod {
    AddSelectClass(id, table) {
        $("#" + id + " tbody").on("click",
            "tr",
            function() {
                if ($(this).hasClass("selected")) {
                    $(this).removeClass("selected");
                } else {
                    // tbCus.$('tr.selected').removeClass('selected');
                    table.$("tr.selected").removeClass("selected");
                    $(this).addClass("selected");
                }
            });
    }
}