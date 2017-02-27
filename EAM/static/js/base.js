var normalPrice = function (p) {
    var formatLen = p < 0 ? 8 : 7;
    p = p.toString();
    var len = p.length;

    for (var i = len; i <= formatLen; i++) {
        p += "0";
    }

    return p.substr(0, formatLen);
};

var lamp = {
    good: function () {
        $("#lamp").removeClass("bed").addClass("good");
    },
    bed: function () {
        $("#lamp").removeClass("good").addClass("bed");
    }
};

var parseDate = function (d) {
    if (!d) return;
    return new Date(parseInt(d.replace("/Date(", "").replace(")/", ""), 10));
};

var addVal = function (reduce, plus, input, delay, min, max, callback) {
    var setInt = null;
    var setTim = null;
    var add = function (span) {
        var ov = parseInt($("#" + input).val());
        if (isNaN(ov))
            ov = min;
        ov += span;
        if (ov > max) ov = max;
        if (ov < min) ov = min;
        $("#" + input).val(ov);
    };
    $("#" + reduce).on("touchstart mousedown", function () {
        add(-1);
        setTim = setTimeout(function () {
            setInt = setInterval(function () { add(-1); }, delay);
        }, 500);
    }).on("touchend mouseup", function () {
        clearTimeout(setTim);
        clearInterval(setInt);
        if (callback) callback();
    });
    $("#" + plus).on("touchstart mousedown", function () {
        add(1);
        setTim = setTimeout(function () {
            setInt = setInterval(function () { add(1); }, delay);
        }, 500);
    }).on("touchend mouseup", function () {
        clearTimeout(setTim);
        clearInterval(setInt);
        if (callback) callback();
    });
};