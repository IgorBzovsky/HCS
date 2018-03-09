export class RegexService {
    get Name() {
        return "^[\u0410-\u0429\u0404\u0406\u0407\u042E\u0406F\u0490][\u0430-\u0449\u0454\u0456\u0457\u044C\u044E\u044F]+$";
    }

    get Password() {
        return '^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[^a-zA-Z\\d\\s:]).{6,100}$';
    }
}