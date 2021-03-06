!function (e, t) {
    if ("function" == typeof define && define.amd) define(["module", "exports", "jquery", "px/util", "px/polyfills"], t); else if ("undefined" != typeof exports) t(module, exports, require("jquery"), require("papaparse"), require("px/util"), require("px/polyfills")); else { var i = { exports: {} }; t(i, i.exports, e.jquery, e.util, e.polyfills), e.pixeladmin = i.exports }
}(this, function (e, t, i, o) {
    "use strict"; function n(e) {
        return e && e.__esModule ? e : { default: e }
    } function r(e, t, i) {
        return t in e ? Object.defineProperty(e, t, {
            value: i, enumerable: !0, configurable: !0, writable: !0
        }) : e[t] = i, e
    } Object.defineProperty(t, "__esModule", {
        value: !0
    }); var s = n(i), u = n(o), l = function (e) {
        var t = {
            isRtl: "rtl" === document.documentElement.getAttribute("dir"), isMobile: /iphone|ipad|ipod|android|blackberry|mini|windows\sce|palm/i.test(navigator.userAgent.toLowerCase()), isLocalStorageSupported: void 0 !== window.Storage, options: {
                resizeDelay: 100, storageKeyPrefix: "px_s_", cookieKeyPrefix: "px_c_"
            }, getScreenSize: function () {
                var e = t._isBreakpointVisible; return e("xs") ? "xs" : e("sm") ? "sm" : e("md") ? "md" : e("lg") ? "lg" : "xl"
            }, storage: {
                _prefix: function (e) {
                    return "" + t.options.storageKeyPrefix + e
                }, set: function (e, i) {
                    var o = "string" == typeof e ? r({}, e, i) : e, n = Object.keys(o); try { for (var s = 0, u = n.length; s < u; s++)window.localStorage.setItem(this._prefix(n[s]), o[n[s]]) } catch (o) { t.cookies.set(e, i) }
                }, get: function (i) { var o = e.isArray(i) ? i : [i], n = {}; try { for (var r = 0, s = o.length; r < s; r++)n[o[r]] = window.localStorage.getItem(this._prefix(o[r])); return e.isArray(i) ? n : n[i] } catch (e) { return t.cookies.get(i) } }
            }, cookies: {
                _prefix: function (e) {
                    return "" + t.options.cookieKeyPrefix + e
                }, set: function (e, t) {
                    for (var i = "string" == typeof e ? r({}, e, t) : e, o = Object.keys(i), n = void 0, s = void 0, u = 0, l = o.length; u < l; u++)n = encodeURIComponent(this._prefix(o[u])), s = encodeURIComponent(i[o[u]]), document.cookie = n + "=" + s
                }, get: function (t) { for (var i = ";" + document.cookie + ";", o = e.isArray(t) ? t : [t], n = {}, r = void 0, s = void 0, l = void 0, d = 0, c = o.length; d < c; d++)r = u.default.escapeRegExp(encodeURIComponent(this._prefix(o[d]))), s = new RegExp(";\\s*" + r + "\\s*=\\s*([^;]+)\\s*;"), l = i.match(s), n[o[d]] = l ? decodeURIComponent(l[1]) : null; return e.isArray(t) ? n : n[t] }
            }, _isBreakpointVisible: function (t) {
                return (document.getElementById("px-breakpoint-" + t) || e('<div id="px-breakpoint-' + t + '"></div>').prependTo(document.body)[0]).offsetTop
            }, _setDelayedResizeListener: function () { var i = e(window), o = null; i.on("resize", function (e) { var i = null; return function () { i && clearTimeout(i), i = setTimeout(function () { i = null, e() }, t.options.resizeDelay) } }(function () { var e = t.getScreenSize(); i.trigger("px.resize"), o !== e && i.trigger("px.screen." + e), o = e })) }
        }; return t._setDelayedResizeListener(), e(function () {
            t.isMobile && window.FastClick && window.FastClick.attach(document.body), t.isRtl && e(window).on("px.resize.px-rtl-fix", function () { document.body.style.overflow = "hidden"; document.body.offsetHeight; document.body.style.overflow = "" }), e(window).trigger("px.load"), u.default.triggerResizeEvent()
        }), t
    }(s.default); window.PixelAdmin = l, t.default = l, e.exports = t.default
});