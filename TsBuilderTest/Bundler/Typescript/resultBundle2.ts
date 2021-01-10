interface IMenuInfluential {
    showMenuBtn: any;
    showMenuBtnHandler(event: any);
}
interface IMenuItemEventable {
    hover(event: any);
    leave(event: any);
}
/// <reference path="interfaces/IMenuInfluential.ts" />
/// <reference path="interfaces/IMenuItemEventable.ts" />

namespace menu {
    export class TopMenu implements IMenuInfluential, IMenuItemEventable {
        private menu: any;
        private topMenuItems: any;
        private readonly _sl = {
            menu: 'j-menu',
            topMenuItem: 'j-top-menu-item',
            subMenu: 'j-sub-menu',
            showMenuBtn: 'j-open-menu-btn'
        };

        showMenuBtn: any;

        hover(e) {
            var hoveredItem = $(e.currentTarget);
            var subMenu = hoveredItem.find('.' + this._sl.subMenu);

            subMenu.show();
        }

        leave(e) {
            var leavedItem = $(e.currentTarget);
            var subMenu = leavedItem.find('.' + this._sl.subMenu);

            subMenu.hide();
        }

        showMenuBtnHandler(e) {
            var target = $(e.target);
            var menuClosed: boolean = this.showMenuBtn.hasClass('menu-closed');

            var showMenuBtnClicked: boolean = target.hasClass(this._sl.showMenuBtn);

            if (menuClosed && !showMenuBtnClicked)
                return;

            if (!menuClosed && this.isClicked(target))
                return;

            if (showMenuBtnClicked && menuClosed) {
                this.showMenuBtn.removeClass('menu-closed');
                this.show();
            }
            else {
                this.showMenuBtn.addClass('menu-closed');
                this.hide();
            }
        }

        private setInstances() {
            this.menu = $('.' + this._sl.menu);
            this.topMenuItems = this.menu.find('.' + this._sl.topMenuItem);
            this.showMenuBtn = $('.' + this._sl.showMenuBtn);
        }

        private setHandlers() {
            this.topMenuItems.on('mouseenter', this.hover.bind(this));
            this.topMenuItems.on('mouseleave', this.leave.bind(this));

            if (this.showMenuBtn.length !== 0) {
                $(window).on('click', this.showMenuBtnHandler.bind(this));
            }
        }

        constructor() {
            this.setInstances();
            this.setHandlers();
        }

        private show() {
            this.menu.removeClass('hide');
        };

        private hide() {
            this.menu.addClass('hide');
        };

        private isClicked(target): boolean {
            return target.hasClass(this._sl.menu) || target.closest('.' + this._sl.menu).length === 1;
        };
    }
}
namespace search {
    export class Search {
        private inputField: any;
        private form: any;
        private readonly _sl = {
            inputField: 'j-search-input',
            form: 'j-search-form'
        };

        constructor() {
            this.setInstances();
            this.setHandlers();
        }

        private setInstances() {
            this.form = $('.' + this._sl.form);
            this.inputField = this.form.find('.' + this._sl.inputField);
        }

        private setHandlers() {
            this.inputField.on('keyup', this.trySearchValueHandler);
        }

        private trySearchValueHandler(e) {
            if (e.which === 13) {
                if (this.inputValueIsValid()) {
                    this.form.submit();
                }
            }
            else {
                return;
            }
        };

        private inputValueIsValid(): boolean {
            if ($.trim(this.inputField.val()) === "")
                return false;
            return true;
        };
    }
}
/// <reference path="menu.ts" />

$(function () {
    new menu.TopMenu();
    new search.Search();
});

namespace Tooltips {

    export function init(options: any[]) {
        $.each(options, function (index, settings) {
            new Tooltip(settings);
        });
    }

    class Tooltip {
        private readonly _defaults = {
            delay: 100,
            contentAsHTML: false
        };
        private settings: any;

        private pluginInit(options) {
            this.settings = $.extend({}, this._defaults, options);

            var tooltipContainer = this.settings.container;

            if (tooltipContainer && tooltipContainer.length === 0) {
                return;
            }

            tooltipContainer.tooltipster(this.settings);
        }

        constructor(options: any) {
            this.pluginInit(options);
        }
    }
}
