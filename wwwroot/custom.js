function printContent(e) {
    var full = document.body.innerHTML;
    var part = document.getElementById(e).innerHTML;
    document.body.innerHTML = part;
    window.print();
    document.body.innerHTML = full;
}