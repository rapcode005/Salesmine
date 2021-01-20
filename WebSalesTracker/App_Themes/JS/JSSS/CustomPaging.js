function CustomPaging(a, b) {
    var Compute = Math.ceil(b.TotalSize / b.PageSize);
    var PageLabel = '<a style = "cursor:pointer" class="page"  page = "{1}">{0}</a>';
    var d = '<span>{0}</span>';
    var PageNumLimit = 10;
    var Start, End;

    if (b.NumPageSize > Compute) {
        b.NumPageSize = Compute;
    }

    var PageListSpan = "";
    if (Compute > 1) {
        var temp = b.PageIndex;

        //First Step
        while (temp % PageNumLimit != 0) {
            temp = temp + 1;
        }

        End = temp < Compute ? temp : Compute;

        //Second Step
        temp = temp - 1;
        while (temp % PageNumLimit != 0) {
            temp = temp - 1;
        }

        Start = temp + 1;

        //Previous Page
        if (b.PageIndex > 1) {
            PageListSpan += PageLabel.replace("{0}", "<<").replace("{1}", "1");
            PageListSpan += PageLabel.replace("{0}", "<").replace("{1}", b.PageIndex - 1);
        } 

        //Last Step
        for (var i = Start; i <= End; i++) {
            if (i == b.PageIndex) {
                PageListSpan += d.replace("{0}", i);
            }
            else {
                PageListSpan += PageLabel.replace("{0}", i).replace("{1}", i);
            }
        }

        //Next Page
        if (b.PageIndex < Compute) {
            PageListSpan += PageLabel.replace("{0}", ">").replace("{1}", b.PageIndex + 1);
            PageListSpan += PageLabel.replace("{0}", ">>").replace("{1}", Compute);
        }
    }

    a.html(PageListSpan);
    try {
        a[0].disabled = false
    }
    catch (m) {
    }
}
(function (a) {
    a.fn.CustomPaging = function (b) {
        var c = {};
        var b = a.extend(c, b);
        return this.each(function () {
            CustomPaging(a(this), b);
        })
    }
})
(jQuery);
