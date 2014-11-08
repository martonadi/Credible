
document.onkeypress = function (event) {
    if (typeof window.event != 'undefined') { // ie
        event = window.event;
        event.target = event.srcElement; // make ie confirm to standards !!
    }
    var kc = event.keyCode;
    var tt = event.target.type;
    if ((kc == 13) && (event.target.getAttribute('enter_ok') == 'true')) {
        return true; // ok
    }

    // alert('kc='+kc+", tt="+tt);
    if ((kc != 8 && kc != 13) || ((tt == 'text' || tt == 'password') && kc != 13) ||
 (tt == 'textarea') || (tt == 'submit' && kc == 13))
        return true;
    //            alert('Bksp/Enter is not allowed here');
    return false;
}

if (typeof window.event != 'undefined') // ie
    document.onkeydown = document.onkeypress; // Trap bksp in ie. !! Note: does not trap enter, but onkeypress does !!


