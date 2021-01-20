//Keydown for ListBox1
function onKeydownListBox1ProductSum(args) {
    var htmlSelect2 = $get('ListBox2');
    if (args.keyCode == 40 || args.keyCode == 38 || args.keyCode == 39) {
        var tempName, tempValue;
        var OkDelete = false;
        for (var i = 0; i < $get('ListBox1').length; i++) {
            if ($get('ListBox1')[i].selected == true) {

                //Arrow Down
                if (args.keyCode == 40) {
                    if (i + 1 < $get('ListBox1').length) {

                        if (FIREFOX)
                            tempName = $get('ListBox1')[i].textContent;
                        else
                            tempName = $get('ListBox1')[i].innerText;

                        tempValue = $get('ListBox1')[i].value;

                        if (FIREFOX)
                            $get('ListBox1')[i].value = $get('ListBox1')[i + 1].textContent;
                        else
                            $get('ListBox1')[i].value = $get('ListBox1')[i + 1].innerText;

                        $get('ListBox1')[i].value = $get('ListBox1')[i + 1].value;

                        if (FIREFOX)
                            $get('ListBox1')[i].textContent = $get('ListBox1')[i + 1].textContent;
                        else
                            $get('ListBox1')[i].innerText = $get('ListBox1')[i + 1].innerText;

                        $get('ListBox1')[i].value = $get('ListBox1')[i + 1].value;

                        if (FIREFOX)
                            $get('ListBox1')[i + 1].textContent = tempName;
                        else
                            $get('ListBox1')[i + 1].innerText = tempName;

                        $get('ListBox1')[i + 1].value = tempValue;

                        $get('ListBox1')[i + 1].selected = true;
                        $get('ListBox1')[i].selected = false;

                        var st = "";

                        for (var i = 0; i <= htmlSelect2.length - 1; i++) {
                            if (i < htmlSelect2.length - 1)
                                st = st + htmlSelect2[i].value + "|";
                            else
                                st = st + htmlSelect2[i].value;
                        }
                        document.getElementById('ListBox2Value').value = st;
                    }
                    break;

                }

                //Arrow Up
                if (args.keyCode == 38) {
                    if (i != 0) {

                        if (FIREFOX)
                            tempName = $get('ListBox1')[i].textContent;
                        else
                            tempName = $get('ListBox1')[i].innerText;

                        tempValue = $get('ListBox1')[i].value;

                        $get('ListBox1')[i].value = $get('ListBox1')[i - 1].value;

                        if (FIREFOX)
                            $get('ListBox1')[i].textContent = $get('ListBox1')[i - 1].textContent
                        else
                            $get('ListBox1')[i].innerText = $get('ListBox1')[i - 1].innerText;

                        $get('ListBox1')[i - 1].value = tempValue;

                        if (FIREFOX)
                            $get('ListBox1')[i - 1].textContent = tempName;
                        else
                            $get('ListBox1')[i - 1].innerText = tempName;

                        $get('ListBox1')[i - 1].selected = true;
                        $get('ListBox1')[i].selected = false;

                        var st = "";

                        for (var i = 0; i <= htmlSelect2.length - 1; i++) {
                            if (i < htmlSelect2.length - 1)
                                st = st + htmlSelect2[i].value + "|";
                            else
                                st = st + htmlSelect2[i].value;
                        }
                        document.getElementById('ListBox2Value').value = st;
                    }
                    break;
                }

                //Arrow right
                if (args.keyCode == 39) {
                    var text = '';
                    if (FIREFOX) {
                        text = $('#ListBox1')[0][i].textContent
                    }
                    else {
                        text = $('#ListBox1')[0][i].innerText
                    }
                    var value = $('#ListBox1')[0][i].value;
                    $('#ListBox2').append('<OPTION selected value="' + value + '">' + text + '</OPTION>');
                    OkDelete = true;
                    $('#ListBox2').focus();
                }

            }
        }

        if (OkDelete == true) {
            $("#ListBox1 > option:selected").each(function () {
                $('#ListBox1 option[value*="' + $(this)[0].value + '"]').remove();
            });
            OkDelete = false;

            var st = "";

            for (var i = 0; i <= htmlSelect2.length - 1; i++) {
                if (i < htmlSelect2.length - 1)
                    st = st + htmlSelect2[i].value + "|";
                else
                    st = st + htmlSelect2[i].value;
            }
            document.getElementById('ListBox2Value').value = st;

            $("#ListBox1").children("option").each(function () {
                $(this)[0].selected = false;
            });

            $("#ListBox2").children("option").each(function () {
                $(this)[0].selected = false;
            });

        }
    }
}

//Keydown for ListBox2
function onKeydownListBox2ProductSum(args) {
    var htmlSelect1 = $get('ListBox1');
    var htmlSelect2 = $get('ListBox2');
    if (args.keyCode == 40 || args.keyCode == 38 || args.keyCode == 37) {
        var tempName, tempValue;
        var OkDelete = false;
        for (var i = 0; i < $get('ListBox2').length; i++) {
            if ($get('ListBox2')[i].selected == true) {

                //Arrow Down
                if (args.keyCode == 40) {
                    if (i + 1 < $get('ListBox2').length) {

                        if (FIREFOX)
                            tempName = $get('ListBox2')[i].textContent;
                        else
                            tempName = $get('ListBox2')[i].innerText;

                        tempValue = $get('ListBox2')[i].value;

                        if (FIREFOX)
                            $get('ListBox2')[i].textContent = $get('ListBox2')[i + 1].textContent;
                        else
                            $get('ListBox2')[i].innerText = $get('ListBox2')[i + 1].innerText;

                        $get('ListBox2')[i].value = $get('ListBox2')[i + 1].value;

                        if (FIREFOX)
                            $get('ListBox2')[i].textContent = $get('ListBox2')[i + 1].textContent;
                        else
                            $get('ListBox2')[i].innerText = $get('ListBox2')[i + 1].innerText;

                        $get('ListBox2')[i].value = $get('ListBox2')[i + 1].value;

                        if (FIREFOX)
                            $get('ListBox2')[i + 1].textContent = tempName;
                        else
                            $get('ListBox2')[i + 1].innerText = tempName;

                        $get('ListBox2')[i + 1].value = tempValue;

                        if (FIREFOX)
                            $get('ListBox2')[i + 1].textContent = tempName;
                        else
                            $get('ListBox2')[i + 1].innerText = tempName;

                        $get('ListBox2')[i + 1].selected = true;
                        $get('ListBox2')[i].selected = false;

                        var st = "";

                        for (var i = 0; i <= htmlSelect2.length - 1; i++) {
                            if (i < htmlSelect2.length - 1)
                                st = st + htmlSelect2[i].value + "|";
                            else
                                st = st + htmlSelect2[i].value;
                        }

                        document.getElementById('ListBox2Value').value = st;
                    }
                    break;

                }

                //Arrow Up
                if (args.keyCode == 38) {
                    if (i != 0) {

                        if (FIREFOX)
                            tempName = $get('ListBox2')[i].textContent;
                        else
                            tempName = $get('ListBox2')[i].innerText;

                        tempValue = $get('ListBox2')[i].value;

                        $get('ListBox2')[i].value = $get('ListBox2')[i - 1].value;

                        if (FIREFOX)
                            $get('ListBox2')[i].textContent = $get('ListBox2')[i - 1].textContent
                        else
                            $get('ListBox2')[i].innerText = $get('ListBox2')[i - 1].innerText;

                        $get('ListBox2')[i - 1].value = tempValue;

                        if (FIREFOX)
                            $get('ListBox2')[i - 1].textContent = tempName;
                        else
                            $get('ListBox2')[i - 1].innerText = tempName;

                        $get('ListBox2')[i - 1].selected = true;
                        $get('ListBox2')[i].selected = false;

                        var st = "";

                        for (var i = 0; i <= htmlSelect2.length - 1; i++) {
                            if (i < htmlSelect2.length - 1)
                                st = st + htmlSelect2[i].value + "|";
                            else
                                st = st + htmlSelect2[i].value;
                        }
                        document.getElementById('ListBox2Value').value = st;
                    }
                    break;
                }

                //Arrow Left
                if (args.keyCode == 37) {
                    var text = '';
                    if (FIREFOX) {
                        text = $('#ListBox2')[0][i].textContent
                    }
                    else {
                        text = $('#ListBox2')[0][i].innerText
                    }
                    var value = $('#ListBox2')[0][i].value;
                    //var value = $(this)[0][i].value;
                    $('#ListBox1').append('<OPTION selected value="' + value + '">' + text + '</OPTION>');
                    OkDelete = true;
                    $('#ListBox1').focus();
                }

            }
        }

        if (OkDelete == true) {
            $("#ListBox2 > option:selected").each(function () {
                $('#ListBox2 option[value*="' + $(this)[0].value + '"]').remove();
            });
            OkDelete = false;

            var st = "";

            for (var i = 0; i <= htmlSelect2.length - 1; i++) {
                if (i < htmlSelect2.length - 1)
                    st = st + htmlSelect2[i].value + "|";
                else
                    st = st + htmlSelect2[i].value;
            }
            document.getElementById('ListBox2Value').value = st;

            $("#ListBox1").children("option").each(function () {
                $(this)[0].selected = false;
            });

            $("#ListBox2").children("option").each(function () {
                $(this)[0].selected = false;
            });

        }
    }
}


//Button >
function onclickButton2ProductSum() {
    var htmlSelect2 = $get('ListBox2');
    var OkDelete = false;
    for (var i = 0; i < $('#ListBox1').find("option").length; i++) {
        if ($('#ListBox1')[0][i].selected == true) {
            var text = '';
            if (FIREFOX) {
                text = $('#ListBox1')[0][i].textContent
            }
            else {
                text = $('#ListBox1')[0][i].innerText
            }
            var value = $('#ListBox1')[0][i].value;
            $('#ListBox2').append('<OPTION selected value="' + value + '">' + text + '</OPTION>');
            OkDelete = true;
        }
    }
    if (OkDelete == true) {
        $("#ListBox1 > option:selected").each(function () {
            $('#ListBox2 option[value*="' + $('#ListBox1')[0].value + '"]').selected = true;
            $('#ListBox1 option[value*="' + $('#ListBox1')[0].value + '"]').remove();
        });
        OkDelete = false;
    }

    var st = "";

    for (var i = 0; i <= htmlSelect2.length - 1; i++) {
        if (i < htmlSelect2.length - 1)
            st = st + htmlSelect2[i].value + "|";
        else
            st = st + htmlSelect2[i].value;
    }
    document.getElementById('ListBox2Value').value = st;
}

//Button >>>
function onclickButton3ProductSum() {
    var htmlSelect2 = $get('ListBox2');
    for (var i = 0; i < $('#ListBox1').find("option").length; i++) {
        var text = '';
        if (FIREFOX) {
            text = $('#ListBox1')[0][i].textContent
        }
        else {
            text = $('#ListBox1')[0][i].innerText
        }
        var value = $('#ListBox1')[0][i].value;
        $('#ListBox2').append('<OPTION selected value="' + value + '">' + text + '</OPTION>');
    }

    for (var i = 0; i < $('#ListBox2').find("option").length; i++) {
        var value = $('#ListBox2')[0][i].value;
        $('#ListBox1 option[value*="' + value + '"]').remove();
    }

    //Remove Selected
    for (var i = 0; i < $('#ListBox2').find("option").length; i++) {
        $('#ListBox2')[0][i].selected = false;
    }

    var st = "";

    for (var i = 0; i <= htmlSelect2.length - 1; i++) {
        if (i < htmlSelect2.length - 1)
            st = st + htmlSelect2[i].value + "|";
        else
            st = st + htmlSelect2[i].value;
    }
    document.getElementById('ListBox2Value').value = st;

}

//Button <
function onclickButton4ProductSum() {
    var htmlSelect2 = $get('ListBox2');
    var Temp;
    var OkDelete = false;
    for (var i = 0; i < $('#ListBox2').find("option").length; i++) {
        if ($('#ListBox2')[0][i].selected == true) {
            var text = '';
            if (FIREFOX) {
                text = $('#ListBox2')[0][i].textContent
            }
            else {
                text = $('#ListBox2')[0][i].innerText
            }
            var value = $('#ListBox2')[0][i].value;
            $('#ListBox1').append('<OPTION selected value="' + value + '">' + text + '</OPTION>');
            OkDelete = true;
        }
    }
    if (OkDelete == true) {
        $("#ListBox2 > option:selected").each(function () {
            $('#ListBox1 option[value*="' + $('#ListBox2')[0].value + '"]').selected = true;
            $('#ListBox2 option[value*="' + $(this)[0].value + '"]').remove();
        });
        OkDelete = false;
    }

    var st = "";

    for (var i = 0; i <= htmlSelect2.length - 1; i++) {
        if (i < htmlSelect2.length - 1)
            st = st + htmlSelect2[i].value + "|";
        else
            st = st + htmlSelect2[i].value;
    }
    document.getElementById('ListBox2Value').value = st;

}

//Button <<<
function onclickButton5ProductSum() {
    var htmlSelect2 = $get('ListBox2');
    var OkDelete = false;
    for (var i = 0; i < $('#ListBox2').find("option").length; i++) {
        var value = $('#ListBox2')[0][i].value;
        var text = '';
        if (FIREFOX) {
            text = $('#ListBox2')[0][i].textContent
        }
        else {
            text = $('#ListBox2')[0][i].innerText
        }
        $('#ListBox1').append('<OPTION selected value="' + value + '">' + text + '</OPTION>');
    }
    if (OkDelete == true) {
        $("#ListBox2").each(function () {
            $('#ListBox2 option[value*="' + $(this)[0].value + '"]').remove();
        });
        OkDelete = false;
    }

    for (var i = 0; i < $('#ListBox1').find("option").length; i++) {
        var value = $('#ListBox1')[0][i].value;
        $('#ListBox2 option[value*="' + value + '"]').remove();
    }

    //Remove Selected
    for (var i = 0; i < $('#ListBox1').find("option").length; i++) {
        $('#ListBox1')[0][i].selected = false;
    }

    document.getElementById('ListBox2Value').value = "";

}


//Move Down
function onclickbtnDownProductSum() {
    var TempValue,TempName;
    var OkDelete = false;
    var htmlSelect2 = $get('ListBox2');

    //ListBox1
    for (var i = 0; i < $get('ListBox1').length; i++) {
        if ($get('ListBox1')[i].selected == true) {
            if (i + 1 < $get('ListBox1').length) {

                if (FIREFOX)
                    TempName = $get('ListBox1')[i].textContent;
                else
                    TempName = $get('ListBox1')[i].innerText;

                TempValue = $get('ListBox1')[i].value;

                if (FIREFOX)
                    $get('ListBox1')[i].value = $get('ListBox1')[i + 1].textContent;
                else
                    $get('ListBox1')[i].value = $get('ListBox1')[i + 1].innerText;

                $get('ListBox1')[i].value = $get('ListBox1')[i + 1].value;

                if (FIREFOX)
                    $get('ListBox1')[i].textContent = $get('ListBox1')[i + 1].textContent;
                else
                    $get('ListBox1')[i].innerText = $get('ListBox1')[i + 1].innerText;

                $get('ListBox1')[i].value = $get('ListBox1')[i + 1].value;

                if (FIREFOX)
                    $get('ListBox1')[i + 1].textContent = TempName;
                else
                    $get('ListBox1')[i + 1].innerText = TempName;

                $get('ListBox1')[i + 1].value = TempValue;

//                if (FIREFOX)
//                    $get('ListBox1')[i + 1].textContent = temp;
//                else
//                    $get('ListBox1')[i + 1].innerText = temp;

                $get('ListBox1')[i + 1].selected = true;
                $get('ListBox1')[i].selected = false;

                var st = "";

                for (var i = 0; i <= htmlSelect2.length - 1; i++) {
                    if (i < htmlSelect2.length - 1)
                        st = st + htmlSelect2[i].value + "|";
                    else
                        st = st + htmlSelect2[i].value;
                }
                document.getElementById('ListBox2Value').value = st;
            }
            break;
        }
    }


    //Listbox2
    for (var i = 0; i < $get('ListBox2').length; i++) {
        if ($get('ListBox2')[i].selected == true) {
            if (i + 1 < $get('ListBox2').length) {

                if (FIREFOX)
                    TempName = $get('ListBox2')[i].textContent;
                else
                    TempName = $get('ListBox2')[i].innerText;

                TempValue = $get('ListBox2')[i].value;

                if (FIREFOX)
                    $get('ListBox2')[i].value = $get('ListBox2')[i + 1].textContent;
                else
                    $get('ListBox2')[i].value = $get('ListBox2')[i + 1].innerText;

                $get('ListBox2')[i].value = $get('ListBox2')[i + 1].value;

                if (FIREFOX)
                    $get('ListBox2')[i].textContent = $get('ListBox2')[i + 1].textContent;
                else
                    $get('ListBox2')[i].innerText = $get('ListBox2')[i + 1].innerText;

                $get('ListBox2')[i].value = $get('ListBox2')[i + 1].value;

                if (FIREFOX)
                    $get('ListBox2')[i + 1].textContent = TempName;
                else
                    $get('ListBox2')[i + 1].innerText = TempName;

                $get('ListBox2')[i + 1].value = TempValue;

//                if (FIREFOX)
//                    $get('ListBox2')[i + 1].textContent = temp;
//                else
//                    $get('ListBox2')[i + 1].innerText = temp;

                $get('ListBox2')[i + 1].selected = true;
                $get('ListBox2')[i].selected = false;

                var st = "";

                for (var i = 0; i <= htmlSelect2.length - 1; i++) {
                    if (i < htmlSelect2.length - 1)
                        st = st + htmlSelect2[i].value + "|";
                    else
                        st = st + htmlSelect2[i].value;
                }
                document.getElementById('ListBox2Value').value = st;
            }
            break;
        }
    }

}


//Move Up
function onclickbtnUpProductSum() {
    var TempName, TempValue;
    var OkDelete = false;
    var htmlSelect2 = $get('ListBox2');

    //ListBox1
    for (var i = 0; i < $get('ListBox1').length; i++) {
        if ($get('ListBox1')[i].selected == true) {
            if (i != 0) {

                if (FIREFOX)
                    TempName = $get('ListBox1')[i].textContent;
                else
                    TempName = $get('ListBox1')[i].innerText;

                TempValue = $get('ListBox1')[i].value;

                $get('ListBox1')[i].value = $get('ListBox1')[i - 1].value;

                if (FIREFOX)
                    $get('ListBox1')[i].textContent = $get('ListBox1')[i - 1].textContent
                else
                    $get('ListBox1')[i].innerText = $get('ListBox1')[i - 1].innerText;

                $get('ListBox1')[i - 1].value = TempValue;

                if (FIREFOX)
                    $get('ListBox1')[i - 1].textContent = TempName;
                else
                    $get('ListBox1')[i - 1].innerText = TempName;

                $get('ListBox1')[i - 1].selected = true;
                $get('ListBox1')[i].selected = false;

                var st = "";

                for (var i = 0; i <= htmlSelect2.length - 1; i++) {
                    if (i < htmlSelect2.length - 1)
                        st = st + htmlSelect2[i].value + "|";
                    else
                        st = st + htmlSelect2[i].value;
                }
                document.getElementById('ListBox2Value').value = st;
            }
            break;
        }
    }

    //Listbox2
    for (var i = 0; i < $get('ListBox2').length; i++) {
        if ($get('ListBox2')[i].selected == true) {
            if (i != 0) {

                if (FIREFOX)
                    TempName = $get('ListBox2')[i].textContent;
                else
                    TempName = $get('ListBox2')[i].innerText;

                TempValue = $get('ListBox2')[i].value;

                $get('ListBox2')[i].value = $get('ListBox2')[i - 1].value;

                if (FIREFOX)
                    $get('ListBox2')[i].textContent = $get('ListBox2')[i - 1].textContent
                else
                    $get('ListBox2')[i].innerText = $get('ListBox2')[i - 1].innerText;

                $get('ListBox2')[i - 1].value = TempValue;

                if (FIREFOX)
                    $get('ListBox2')[i - 1].textContent = TempName;
                else
                    $get('ListBox2')[i - 1].innerText = TempName;

                $get('ListBox2')[i - 1].selected = true;
                $get('ListBox2')[i].selected = false;

                var st = "";

                for (var i = 0; i <= htmlSelect2.length - 1; i++) {
                    if (i < htmlSelect2.length - 1)
                        st = st + htmlSelect2[i].value + "|";
                    else
                        st = st + htmlSelect2[i].value;
                }
                document.getElementById('ListBox2Value').value = st;
            }
            break;
        }
    }
}