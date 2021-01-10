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