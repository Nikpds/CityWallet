import { Injectable } from '@angular/core';

@Injectable()
export class LoaderService {

    private openJobs = 0;
    private _selector = 'conloader';
    private _element: HTMLElement;

    constructor() {
        this._element = document.getElementById(this._selector);
    }

    public show(): void {
        this._element.style['display'] = 'block';
        this.openJobs += 1;
    }

    public hide(delay = 0): void {
        this.openJobs -= 1;
        if (this.openJobs < 1) {
            setTimeout(() => {
                this._element.style['display'] = 'none';
            }, delay);
        }
    }
}