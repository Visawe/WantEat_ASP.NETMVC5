$(function () {
    $("#accordion").accordion({
        heightStyle: "content",
        autoHeight: false,
        clearStyle: true,
        collapsible: true,
        active: false
        
    });

    /* ----------------------------------------------------------- */
    /*  13. GRID AND LIST LAYOUT CHANGER 
    /* ----------------------------------------------------------- */

    $("#aa-list-properties").click(function (e) {
        e.preventDefault(e);
        jQuery(".aa-properties-nav").addClass("aa-list-view");
    });
    $("#aa-grid-properties").click(function (e) {
        e.preventDefault(e);
        jQuery(".aa-properties-nav").removeClass("aa-list-view");
    });
});