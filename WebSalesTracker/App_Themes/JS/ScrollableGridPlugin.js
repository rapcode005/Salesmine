/*!
* This plug-in is developed for ASP.Net GridView control to make the GridView scrollable with Fixed headers.
*/
(function ($) {
    $.fn.Scrollable = function (options) {
        var defaults = {
            ScrollHeight: 300
        };
        var options = $.extend(defaults, options);
        return this.each(function () {
            var t = $(this).get(0);
            var gridWidth = t.offsetWidth;
            var CurrentWidth = new Array();
            var parentDiv = t.parentNode;
            for (var index = 0; index < t.getElementsByTagName("TH").length; index++) {
                CurrentWidth[index] = t.getElementsByTagName("TH")[index].offsetWidth;
            }

            var t2 = t.cloneNode(true);
            for (i = t2.rows.length - 1; i > 0; --i)
                t2.deleteRow(i);

            var gridRow = t.getElementsByTagName("TR")[1];
            for (var index = 0; index < gridRow.getElementsByTagName("TD").length; index++) {

                gridRow.getElementsByTagName("TD")[index].style.width = CurrentWidth[index] + "px";
                //t2.getElementsByTagName("TH")[index].style.height = CurrentHeight[index] + "px";
                t2.getElementsByTagName("TH")[index].style.width = CurrentWidth[index] + "px";
            }

            t.getElementsByTagName("TR")[0].style.visibility = "hidden";

            t.getElementsByTagName("TR")[0].style.height = "0px";
            t.getElementsByTagName("TR")[0].style.width = "0px";

            var dummyHeader = document.createElement("div");
            dummyHeader.appendChild(t2);
            parentDiv.appendChild(dummyHeader);
            //            if (options.Width > 0) {
            //                gridWidth = options.Width;
            //            }
            var scrollableDiv = document.createElement("div");
            gridWidth = parseInt(gridWidth) + 17;
            scrollableDiv.style.cssText = "";
            scrollableDiv.style.cssText = "overflow-y:scroll;height:" + options.ScrollHeight + "px;width:" + gridWidth + "px";
            scrollableDiv.appendChild(t);
            parentDiv.appendChild(scrollableDiv);
            parentDiv.style.cssText = "direction: rtl;";
        });
    };
})(jQuery);