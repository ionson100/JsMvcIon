(function ($) {
    var valid;
    $.fn.Rendering = function (options) {
        var settings = $.extend({
            'Model': null,
            'Table': false,
          
        }, options);
        var obj = jQuery.parseJSON(settings.Model);
        var table = $('<table></table>').addClass('foo');
        valid = settings.Validate;

        if (settings.Table == true) {
            var isk = group(obj);
            for (var i = 0; i < isk.max; i++) {
                var row = $('<tr></tr>');
                for (var ii = 0; ii < isk.res.length; ii++) {
                    var cell = $('<td></td>');
                    if (isk.res[ii].length <= i) {
                        cell.append('');
                    } else {
                        cell.append(isk.res[ii][i].element);
                    }
                    row.append(cell);
                }
                table.append(row);
            }
        } else {
            $.each(obj, function () {
                var rows = $('<tr></tr>').addClass('bar').append(this.element);
                table.append(rows);
            });
        }
        $(this).append(table);
    };

  
       

    function group(o) {
        var w1 = [], w2 = [], w3 = [], w4 = [], w5 = [], w6 = [];
        var res = [];

        $.each(o, function (key, val) {
            if (0 <= val.sorted && val.sorted <= 100) w1.push(val);
            if (100 < val.sorted && val.sorted <= 200) w2.push(val);
            if (200 < val.sorted && val.sorted <= 300) w3.push(val);
            if (300 < val.sorted && val.sorted <= 400) w4.push(val);
            if (400 < val.sorted && val.sorted <= 500) w5.push(val);
            if (500 < val.sorted && val.sorted <= 600) w6.push(val);
        });
        var imax = 0;
        if (w1.length > 0) {
            res.push(w1);
            if (imax <= w1.length) imax = w1.length;
        }
        if (w2.length > 0) {
            res.push(w2);
            if (imax <= w2.length) imax = w2.length;
        }
        if (w3.length > 0) {
            res.push(w3);
            if (imax <= w3.length) imax = w3.length;
        }
        if (w4.length > 0) {
            res.push(w4);
            if (imax <= w4.length) imax = w4.length;
        }
        if (w5.length > 0) {
            res.push(w5);
            if (imax <= w5.length) imax = w5.length;
        }
        if (w6.length > 0) {
            res.push(w6);
            if (imax <= w6.length) imax = w6.length;
        }
        var rr = {};
        rr['max'] = imax;
        rr['res'] = res;
        return rr;
    }

   

  
  

})(jQuery);

function getJsonJs() {
    var tt = $('[data-js]');
    var objs = {};
    jQuery.each(tt, function () {
        var atr = this.getAttribute('data-js');
        if (atr == 0 || atr == 1 || atr == 5) {
            objs['' + this.getAttribute('name') + ''] = this.value;
        }
        if (atr == 3 && this.checked == true) {
            objs['' + this.getAttribute('name') + ''] = this.value;
        }
        if (atr == 4 && this.multiple == false) {
            objs['' + this.getAttribute('name') + ''] = this.value;
        }
        if (atr == 4 && this.multiple == true) {

            var arr = [];
            if (jQuery.browser == "msie") {
                for (var i = 0; i < this.all.length; i++) {
                    if (this.all.item(i).selected == true) {
                        arr.push(this.all.item(i).value);
                    }
                }
                objs['' + this.getAttribute('name') + ''] = arr;
            }
            objs['' + this.getAttribute('name') + ''] = $(this).val();

        }
        if (atr == 2) {
            objs['' + this.getAttribute('name') + ''] = this.checked;
        }

    });
      alert(JSON.stringify(objs));
    return JSON.stringify(objs);
}



