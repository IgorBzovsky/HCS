export class RegexService {
    get Name() {
        return "^[А-ЩЄІЇЮЯ][а-щєіїьюя]+$";
    }

    get SpacedName() {
        return "^[А-ЩЄІЇЮЯ][А-ЩЄІЇЮЯа-щєіїьюя ]+$";
    }

    get Decimal() {
        return "^\\d+(?:[.]\\d{1,2})?$";
    }

    get Password() {
        return '^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[^a-zA-Z\\d\\s:]).{6,100}$';
    }
}