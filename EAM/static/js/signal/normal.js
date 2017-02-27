$(function () {
    var calcPrice = function (ifTp) {
        var p, v, f;
        var ifBuy = $("#submit").hasClass("btnbuy");
        if (ifBuy) {
            p = parseFloat($("#buyPrice").html());
        } else {
            p = parseFloat($("#sellPrice").html());
        }

        f = p > 50 ? 0.01 : 0.0001;

        if (ifTp) {
            v = parseInt($("#tpPoint").val());
            $("#tpPrice").html(normalPrice(p + v * f * (ifBuy ? 1 : -1)));
        }
        else {
            v = parseInt($("#slPoint").val());
            $("#slPrice").html(normalPrice(p - v * f * (ifBuy ? 1 : -1)));
        }
    };

    var calcSpread = function () {
        var ask = parseFloat($("#buyPrice").html());
        var bid = parseFloat($("#sellPrice").html());
        var f = 100000;
        if (!isNaN(ask) && !isNaN(bid)) {
            if (ask > 50)
                f = 1000;
            $("#spread").html(parseInt(ask * f - bid * f));
        }
    };

    addVal("tpReduce", "tpPlus", "tpPoint", 20, 1, 1000, function () { calcPrice(true); });
    addVal("slReduce", "slPlus", "slPoint", 20, 1, 1000, function () { calcPrice(false); });
    addVal("wrReduce", "wrPlus", "wr", 20, 1, 100);

    $("#symbol").change(function () {
        lamp.bed();
        $("#buyPrice").html("0");
        $("#spread").html("0");
        $("#sellPrice").html("0");
        $("#slPrice").html("");
        $("#tpPrice").html("");
    });

    var submitSignal = {
        init: function () {
            $(".tab-buy-box").click(function () {
                $(".tab-sell-box").removeClass("boxactive");
                $(this).addClass("boxactive");
                $("#submit").removeClass("btnsell").addClass("btnbuy").html("提交买涨信号");
                calcPrice(true);
                calcPrice(false);
            });

            $(".tab-sell-box").click(function () {
                $(".tab-buy-box").removeClass("boxactive");
                $(this).addClass("boxactive");
                $("#submit").removeClass("btnbuy").addClass("btnsell").html("提交买跌信号");
                calcPrice(true);
                calcPrice(false);
            });

            $("#submit").click(submitSignal.submit);
        },

        submit: function () {
            $("#submit").prop("disabled", true);
            var ifBuy = $("#submit").hasClass("btnbuy");

            var data = {
                symbol: $("#symbol").val(),
                tradeType: ifBuy ? 0 : 1,
                source: $("#source").val(),
                tpPoints: $("#tpPoint").val(),
                slPoints: $("#slPoint").val(),
                winningRate: $("#wr").val(),
                expectPrice: parseFloat(ifBuy ? $("#buyPrice").html() : $("#sellPrice").html())
            };

            if (data.symbol.length == 0) {
                alert("请选择商品");
                $("#submit").prop("disabled", false);
                return;
            }

            if (isNaN(data.expectPrice) || data.expectPrice <= 0) {
                alert("交易价格无效");
                $("#submit").prop("disabled", false);
                return;
            }

            if (isNaN(data.tpPoints) || isNaN(data.slPoints) || data.tpPoints < data.slPoints) {
                alert("止盈点或止损点无效(止盈点和止损点必须为整数，且止盈>止损)");
                $("#submit").prop("disabled", false);
                return;
            }

            $.ajax({
                url: "/signal/submitnormal/",
                data: data,
                type: "POST",
                dataType: "json",
                timeout: 10000,
                beforeSend: function () { },
                success: function (data) {
                    if (data && data.ErrorCode.length == 0) {
                        alert("提交信号成功");
                        $("#symbol").val("");
                    } else {
                        alert("提交失败：" + data.ErrorCode);
                    }
                },
                error: function (xhr, errorType, error) {
                    alert("提交失败：" + (error || "网络超时"));
                },
                complete: function () { $("#submit").prop("disabled", false); }
            });
        }
    };
    submitSignal.init();

    var priceUpdate = {
        lock: false,
        setInt: null,
        init: function () {
            priceUpdate.ajax();
            priceUpdate.setInt = setInterval(priceUpdate.ajax, 3000);
        },
        ajax: function () {
            if (priceUpdate.lock)
                return;

            var _symbol = $("#symbol").val();
            if (!_symbol)
                return;

            priceUpdate.lock = true;
            $.ajax({
                url: "/info/symbolprice/",
                data: { symbol: _symbol },
                type: "GET",
                dataType: "json",
                timeout: 10000,
                beforeSend: function () { },
                success: function (data) {
                    if (data && !data.ErrorCode) {
                        lamp.good();
                        $("#buyPrice").html(normalPrice(data.Ask));
                        $("#sellPrice").html(normalPrice(data.Bid));
                        calcPrice(true);
                        calcPrice(false);
                        calcSpread();
                    } else {
                        lamp.bed();
                    }
                },
                error: function (xhr, errorType, error) {
                    lamp.bed();
                },
                complete: function () {
                    priceUpdate.lock = false;
                }
            });
        }
    };
    priceUpdate.init();

});