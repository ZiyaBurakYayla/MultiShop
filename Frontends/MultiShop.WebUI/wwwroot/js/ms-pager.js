(function () {
    function buildPager(container) {
        var pageSize = parseInt(container.getAttribute('data-ms-pager')) || 10;
        var items = [];
        for (var i = 0; i < container.children.length; i++) {
            items.push(container.children[i]);
        }
        if (items.length <= pageSize) {
            return;
        }

        var pageCount = Math.ceil(items.length / pageSize);
        var current = 1;

        var nav = document.createElement('div');
        nav.className = 'ms-pager';

        var host = container.closest('.ms-table-wrap');
        if (host && host.parentNode) {
            host.parentNode.insertBefore(nav, host.nextSibling);
        } else if (container.parentNode) {
            container.parentNode.insertBefore(nav, container.nextSibling);
        }

        function render() {
            for (var i = 0; i < items.length; i++) {
                var visible = i >= (current - 1) * pageSize && i < current * pageSize;
                items[i].style.display = visible ? '' : 'none';
            }

            nav.innerHTML = '';

            var info = document.createElement('span');
            info.className = 'ms-pager-info';
            var from = (current - 1) * pageSize + 1;
            var to = Math.min(current * pageSize, items.length);
            info.innerText = from + '-' + to + ' / ' + items.length;
            nav.appendChild(info);

            var buttons = document.createElement('div');
            buttons.className = 'ms-pager-buttons';
            nav.appendChild(buttons);

            buttons.appendChild(makeButton('‹', current > 1, function () { go(current - 1); }, false));

            var start = Math.max(1, current - 2);
            var end = Math.min(pageCount, start + 4);
            start = Math.max(1, end - 4);

            if (start > 1) {
                buttons.appendChild(makeButton('1', true, function () { go(1); }, false));
                if (start > 2) {
                    buttons.appendChild(makeDots());
                }
            }

            for (var p = start; p <= end; p++) {
                (function (page) {
                    buttons.appendChild(makeButton(String(page), true, function () { go(page); }, page === current));
                })(p);
            }

            if (end < pageCount) {
                if (end < pageCount - 1) {
                    buttons.appendChild(makeDots());
                }
                buttons.appendChild(makeButton(String(pageCount), true, function () { go(pageCount); }, false));
            }

            buttons.appendChild(makeButton('›', current < pageCount, function () { go(current + 1); }, false));
        }

        function makeDots() {
            var span = document.createElement('span');
            span.className = 'ms-pager-dots';
            span.innerText = '…';
            return span;
        }

        function makeButton(text, enabled, action, active) {
            var button = document.createElement('button');
            button.type = 'button';
            button.className = 'ms-pager-btn' + (active ? ' is-active' : '');
            button.innerText = text;
            if (!enabled) {
                button.disabled = true;
            } else {
                button.addEventListener('click', action);
            }
            return button;
        }

        function go(page) {
            if (page < 1 || page > pageCount) {
                return;
            }
            current = page;
            render();
        }

        render();
    }

    function init() {
        var containers = document.querySelectorAll('[data-ms-pager]');
        for (var i = 0; i < containers.length; i++) {
            buildPager(containers[i]);
        }
    }

    if (document.readyState === 'loading') {
        document.addEventListener('DOMContentLoaded', init);
    } else {
        init();
    }
})();
