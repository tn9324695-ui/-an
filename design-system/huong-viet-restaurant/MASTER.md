# MASTER DESIGN SYSTEM: L'ATELIER FINE DINING (ATELIER ESPRESSO)

> **Source**: Extracted directly from uploaded Stitch Design Package (`stitch_website_nh_h_ng_t_i_gi_n.zip` — `atelier_espresso/DESIGN.md` & `haute_gastronomie_poise/DESIGN.md`).

---

## 1. Brand & Aesthetic Direction
- **Brand Name**: **L'Atelier** — Haute Cuisine & Contemporary Fine Dining (`128 Đồng Khởi, Quận 1, TP. Hồ Chí Minh`)
- **Visual Language**: **Quiet Luxury Minimalism** infused with **Classical French Editorial Typography**
- **Atmosphere**: Candlelit dining salon at dusk—warm roasted espresso, aged cognac/hazelnut, raw linen, and hand-milled porcelain cream (`#FCF9F4`).

---

## 2. Official Color Tokens (`Atelier Espresso`)

| Token Name | Hex Code | Role & Usage |
| :--- | :--- | :--- |
| `primary` | `#130803` | Deep Roasted Espresso — Primary headings, primary CTA buttons, active states, formal crests |
| `primary-container` | `#2C1E16` | Espresso Container — Tasting menu feature banners, VIP membership card background |
| `primary-fixed` | `#F7DDD0` | Warm Champagne Tint — Subtitles and text over dark espresso containers |
| `primary-fixed-dim` | `#DAC2B5` | Muted Champagne — Secondary text/metadata on dark backgrounds |
| `secondary` | `#765844` | Warm Hazelnut & Aged Cognac — Section index labels (`01 — TRIẾT LÝ`), icons, hover accents |
| `secondary-container` | `#FDD5BC` | Soft Cognac Wash — Icon circles, ambient blur glows |
| `secondary-fixed` | `#FFDCC6` | Warm Peach/Cognac Badge — Course tags, active filter highlights |
| `surface` / `background` | `#FCF9F4` | Warm Porcelain Cream — Main page background canvas (replaces stark white) |
| `surface-container-lowest` | `#FFFFFF` | Pure White — Elevated form containers, floating cards |
| `surface-container-low` | `#F6F3EE` | Raw Linen — Alternating section backgrounds, reservation step cards |
| `surface-container` | `#F0EDE9` | Deeper Linen — Dish cards, secondary buttons, input backgrounds |
| `surface-container-high` | `#EBE8E3` | Pressed / Inactive slot surface |
| `on-surface` | `#1C1C19` | Primary Body Copy |
| `on-surface-variant` | `#4E4540` | Secondary Body Copy & Descriptions |
| `outline` | `#80756F` | Form borders & subtle metadata |
| `outline-variant` | `#D2C4BD` | `1px` hairline dividers (`30%–50%` opacity) |
| `error` | `#BA1A1A` | Form validation & destructive actions |

---

## 3. Typography & Iconography
- **Display & Headings**: `Playfair Display` (with `EB Garamond` / `Cormorant Garamond` accents)
  - `display-lg`: `64px` / line-height `72px` / weight `400` / letter-spacing `-0.02em`
  - `headline-lg`: `44px` / line-height `52px` / weight `400` / letter-spacing `-0.01em`
  - `headline-md`: `32px` / line-height `40px` / weight `400`
  - `headline-sm`: `24px` / line-height `32px` / weight `500`
- **Body & UI Labels**: `DM Sans`
  - `title-lg`: `20px` (`500`), `title-md`: `16px` (`500`)
  - `body-lg`: `18px` (`400`), `body-md`: `15px` (`400`)
  - `label-md`: `12px` (`500`, uppercase, `letter-spacing: 0.12em`)
  - `label-sm`: `11px` (`500`, uppercase, `letter-spacing: 0.14em`)
- **Icons**: Google `Material Symbols Outlined`

---

## 4. GSAP 3.12.5 Motion Choreography
- **Hero Reveal**: Subtle fade-up (`y: 20`, `opacity: 0`, `duration: 0.7`, `ease: 'power2.out'`, `stagger: 0.1`).
- **Editorial Sections & Dish Carousel (`ScrollTrigger`)**: Smooth reveal as sections enter viewport (`start: 'top 85%'`).
- **6-Step Reservation Flow & Membership Card**: Interactive state transitions and live card preview updates.
