var px = {
    cookie: {
        set: function (name, value, day) {
            //设置
            if (!day) {
                day = 30
            }
            var exp = new Date();
            exp.setTime(exp.getTime() + day * 24 * 60 * 60 * 1000);
            document.cookie = name + "=" + escape(value) + ";expires=" + exp.toGMTString() + ';path=/';
        }, get: function (name) {
            //获取
            var arr, reg = new RegExp("(^| )" + name + "=([^;]*)(;|$)");

            if (arr = document.cookie.match(reg))

                return unescape(arr[2]);
            else
                return null;
        }, del: function (name) {
            //移除
            var exp = new Date();
            exp.setTime(exp.getTime() - 1);
            var cval = getCookie(name);
            if (cval != null)
                document.cookie = name + "=" + cval + ";expires=" + exp.toGMTString();
        }
    },
    device: {
        cookieName: 'deviceId',
        getBrowserInfo: function appInfo() {
            var browser = { appname: 'unknown', version: 0 },
                userAgent = window.navigator.userAgent.toLowerCase();  // 使用navigator.userAgent来判断浏览器类型
            //msie,firefox,opera,chrome,netscape  
            if (/(msie|firefox|opera|chrome|netscape)\D+(\d[\d.]*)/.test(userAgent)) {
                browser.appname = RegExp.$1;
                browser.version = RegExp.$2;
            } else if (/version\D+(\d[\d.]*).*safari/.test(userAgent)) { // safari  
                browser.appname = 'safari';
                browser.version = RegExp.$2;
            }

            return browser;
        },
        getDeviceInfo: function () {
            var deviceId = px.device.fit();
            var bInfo = px.device.getBrowserInfo();
            var info = {
                id: deviceId,
                name: bInfo.appname + " " + bInfo.version,
            };
            return info;
        },
        fit: function () {
            //检查/设置设备id
            var deviceId = px.cookie.get(px.device.cookieName);
            if (!deviceId) {
                deviceId = px.util.newGuid();
                px.cookie.set(px.device.cookieName, deviceId);
            }
            return deviceId;
        }
    },
    util: {
        newGuid: function (excludeBar) {
            var guid = "";
            for (var i = 1; i <= 32; i++) {
                var n = Math.floor(Math.random() * 16.0).toString(16);
                guid += n;
                if (!excludeBar && ((i == 8) || (i == 12) || (i == 16) || (i == 20))) {
                    guid += "-";
                }
            }
            return guid;
        },
        date: {
            toString: function (fmt, d) {
                var o = {
                    "M+": d.getMonth() + 1,                 //月份
                    "d+": d.getDate(),                    //日
                    "h+": d.getHours(),                   //小时
                    "m+": d.getMinutes(),                 //分
                    "s+": d.getSeconds(),                 //秒
                    "q+": Math.floor((d.getMonth() + 3) / 3), //季度
                    "f": d.getMilliseconds()             //毫秒
                };
                if (/(y+)/.test(fmt))
                    fmt = fmt.replace(RegExp.$1, (d.getFullYear() + "").substr(4 - RegExp.$1.length));
                for (var k in o)
                    if (new RegExp("(" + k + ")").test(fmt))
                        fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
                return fmt;
            }
        }
    },
    ajax: {
        interceptor: function (res, failFunc) {
            //处理ajax结果
            if (res.ErrCode === 4) {
                //InvalidLoginToken
                location.href = '/User/Login';
                throw res.ErrMsg;
            }
            if (res.ErrCode) {
                alert(res.ErrMsg);
                if (failFunc) {
                    failFunc(res);
                }
                throw new Error(res.ErrMsg);
            }
        }
    },
    commonTable: {
        trClick: function () {
            //行监听
            $('table.common tbody tr').click(function () {
                $('table.common tr').removeClass('current');
                $(this).addClass('current');
            })
        },
        hoverTitle: function (columnClassArray) {
            //指定将text()提示为title
            //columnClassArray - 列td指定的class名
            var selectorArray = [];
            $.each(columnClassArray, function (i, v) {
                selectorArray.push('table.common td.' + v);
            })
            var selector = selectorArray.join();
            $(selector).hover(function () {
                $(this).attr('title', $(this).text());
            }, function () {
                $(this).attr('title', null);
            })
        },
    },
    time: {
        expendStartEndTime: function (selector) {
            //开始、结束时间扩张选择菜单
            //指定的选择器元素必须有id
            $(selector).click(function () {
                var p = $(this).parent();
                if (!p.has('ul.dropdown-menu').length) {
                    var id = $(this).attr('id');
                    var time = $(this).text();
                    p.append('\
<ul class="dropdown-menu" aria-labelledby="'+ id + '" data-time="' + time + '">\
    <li><a class="pointer" onclick="px.time.setStartEndTime(this, 0, 1)">查询1秒内</a></li>\
    <li><a class="pointer" onclick="px.time.setStartEndTime(this, 1, 1)">查询前后1秒</a></li>\
    <li><a class="pointer" onclick="px.time.setStartEndTime(this, 2, 2)">查询前后2秒</a></li>\
    <li><a class="pointer" onclick="px.time.setStartEndTime(this, 10, 10)">查询前后10秒</a></li>\
    <li><a class="pointer" onclick="px.time.setStartEndTime(this, 30, 30)">查询前后30秒</a></li>\
    <li><a class="pointer" onclick="px.time.setStartEndTime(this, 60, 60)">查询前后1分钟</a></li>\
    <li><a class="pointer" onclick="px.time.setStartEndTime(this, 120, 120)">查询前后2分钟</a></li>\
    <li><a class="pointer" onclick="px.time.setStartEndTime(this, 240, 240)">查询前后4分钟</a></li>\
    <li><a class="pointer" onclick="px.time.setStartEndTime(this, 480, 480)">查询前后8分钟</a></li>\
</ul>\
')
                }
            })
        },
        setStartEndTime: function (el, startTimeMinusSec, endTimeAddSec, startSel, endSel) {
            //开始、结束时间扩张填写
            //startTimeMinusSec、endTimeAddSec - 增减的秒数
            //startSel、endSel - 开始、结束的时间控件选择器
            var time = new Date($(el).parents('ul.dropdown-menu').data('time')).getTime();
            var startTime = time - startTimeMinusSec * 1000;
            var endTime = time + endTimeAddSec * 1000;
            startSel = startSel == undefined ? '#startTime' : startSel;
            endSel = endSel == undefined ? '#endTime' : endSel;
            $(startSel).val(px.util.date.toString('yyyy-MM-dd hh:mm:ss.f', new Date(startTime))).change();
            $(endSel).val(px.util.date.toString('yyyy-MM-dd hh:mm:ss.f', new Date(endTime))).change();
        },
        timePlus: function () {
            //时间扩张按钮
            //plus-id - 指定增减的控件选择器
            //plus-sec - 增减的秒数
            $('.px-time-plus').click(function () {
                var id = $(this).data('plus-id');
                var plusSec = $(this).data('plus-sec');
                var timeStr = $('#' + id).val();
                var time = new Date(timeStr);
                if (time == 'Invalid Date') {
                    time = new Date();
                }
                time = new Date(time.getTime() + plusSec * 1000);

                $('#' + id).val(px.util.date.toString('yyyy-MM-dd hh:mm:ss.f', time)).change();
            })
        },
    },
};
$(function () {
    px.time.timePlus();
})