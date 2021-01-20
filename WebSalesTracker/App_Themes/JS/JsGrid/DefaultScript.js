function getCookie(c_name) {
    var i, x, y, ARRcookies = document.cookie.split(";");
    for (i = 0; i < ARRcookies.length; i++) {
        x = ARRcookies[i].substr(0, ARRcookies[i].indexOf("="));
        y = ARRcookies[i].substr(ARRcookies[i].indexOf("=") + 1);
        x = x.replace(/^\s+|\s+$/g, "");
        if (x == c_name) {
            return unescape(y);
        }
    }
}

function setCookie(c_name, value, exdays) {
    var exdate = new Date();
    exdate.setDate(exdate.getDate() + exdays);
    var c_value = escape(value) + ((exdays == null) ? "" : "; expires=" + exdate.toUTCString());
    document.cookie = c_name + "=" + c_value + ';path=/';
}

function checkCookie() {
    var username = getCookie("username");
    if (username != null && username != "") {
        alert("Welcome again " + username);
    }
    else {
        username = prompt("Please enter your name:", "");
        if (username != null && username != "") {
            setCookie("username", username, 365);
        }
    }
}

function OrderHistoryClick() {
    setCookie('CSS', "order_history", 1);

    setCookie('CProjID', "", -1);
}

function QuoteClick() {
    setCookie('CSS', "Quotes", 1);
    setCookie('CProjID', "", -1);
}

function Quote1KClick() {
    setCookie('CSS', "Quotes1K", 1);
    setCookie('CProjID', "", -1);
}

function productClick() {
    setCookie('CSS', "product", 1);
    setCookie('CProjID', "", -1);
}


function productTClick() {
    setCookie('CSS', "productT", 1);
    setCookie('CProjID', "", -1);
}

function NotesClick() {
    setCookie('CSS', "Notes", 1);
    setCookie('CProjID', "", -1);
}

function CustomerManClick() {
    setCookie('CSS', "CustomerMan", 1);
    setCookie('CProjID', "", -1);
}

function CustomerClick() {
    setCookie('CSS', "Customer", 1);
    setCookie('CProjID', "", -1);
}

function NotesTClick() {
    setCookie('CSS', "NotesT", 1);
    setCookie('CProjID', "", -1);
}

function QuotesPipeline() {
    setCookie('CSS', "QuotesPipeline", 1);
    setCookie('CProjID', "", -1);
}

function ConstClick() {
    setCookie('CSS', "Construction", 1);
    // setCookie('CSS', "Construction", 1);
}

function QuotesGuidance() {
    setCookie('CSS', "QuoteGuidance", 1);
    setCookie('CProjID', "", -1);
}

function MiningClick() {
    setCookie('CSS', "Mining", 1);
    // setCookie('CSS', "Construction", 1);
}

function CustomerLookupClick() {
    setCookie('CSS', "", -1);
    setCookie('CProjID', "", -1);
}

function adminClick() {
    setCookie('CSS', "", -1);
    setCookie('CProjID', "", -1);
}

function OnHoldOrderClick() {
    setCookie('CSS', "OnHoldOrder", 1);
    setCookie('CProjID', "", -1);
}

function btnClick() {
    setCookie('CSS', "", -1);
    setCookie('CProjID', "", -1);
}





function Assign() {
    PageMethods.GetData(document.getElementById('htdAccountValue').value);
}

function productCaution() {
    var ans = confirm('Caution: Proceeding to the next window might take long time to load. \n Click ok to Continue.');
    if (ans == true) {
        window.opener.location = '../ProductSummary/ProductSummaryT.aspx';
    }
}


function notesTCaution() {
    var ans = confirm('Caution: Proceeding to the next window might take long time to load. \n Click ok to Continue.');
    if (ans == true) {
        window.opener.location = '../NotesCommHistory/NotesCommHistoryT.aspx';
    }

}

function ReloadtheparentWindow() {
    window.opener.location.reload(true);

}


