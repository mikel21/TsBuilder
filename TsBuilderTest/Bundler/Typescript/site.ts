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