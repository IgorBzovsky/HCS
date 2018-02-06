import { Component, OnInit } from '@angular/core';
import { TestService } from "../../services/test.service";
import { AuthService } from "../../services/auth.service";

@Component({
    selector: 'protected',
    templateUrl: './protected.component.html'
})

export class ProtectedComponent implements OnInit {
    tests: any[];
    constructor(private testService: TestService, private authService: AuthService) {  }
    ngOnInit() {
        
    }

    show() {
        this.testService.getTests().subscribe(tests => {
            this.tests = tests,
                console.log(this.tests)
        });
        console.log(this.authService.getAuthorizationHeaderValue())
    }
}