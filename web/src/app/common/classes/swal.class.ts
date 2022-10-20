import { Injectable } from '@angular/core';
import swal, { SweetAlertIcon, SweetAlertResult } from 'sweetalert2';

@Injectable()
export class SwalClass{
    constructor() {}

    public showAlert(title?: string, text?: string,  icon?: SweetAlertIcon, onClose?: () => void): void {
        swal.fire({ title, text, icon }).then(() => {
            if (onClose && typeof (onClose) === 'function') {
                onClose();
            }
        });
    }

    public showConfirm(title?: string, text?: string, onConfirm?: () => void, icon?: SweetAlertIcon,
    // tslint:disable-next-line:align
    buttonLabels?: { yes: string, no: string }) {
    if (!title) { title = ''; }
    if (!text) { text = ''; }
    if (!icon) { icon = 'warning'; }
    if (!buttonLabels) { buttonLabels = { yes: 'Yes', no: 'No' }; }

    swal.fire({
        title,
        text,
        icon,
        showCancelButton: true,
        confirmButtonText: buttonLabels.yes,
        cancelButtonText: buttonLabels.no
    }).then((result: SweetAlertResult) => {
        if (result.value && onConfirm && typeof (onConfirm) === 'function') {
            onConfirm();
        }
    });
}
}