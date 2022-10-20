import { Directive, ElementRef, HostListener, Renderer2} from '@angular/core';

@Directive({
  selector: '[appCollapseable]'
})
export class CollapseableDirective {
  constructor(private renderer: Renderer2,private el: ElementRef) {}

  @HostListener('click') onClick() {
    this.el.nativeElement.classList.contains('collapsed') ? this.renderer.removeClass(this.el.nativeElement, 'collapsed') : this.renderer.addClass(this.el.nativeElement, 'collapsed');
  }
}
