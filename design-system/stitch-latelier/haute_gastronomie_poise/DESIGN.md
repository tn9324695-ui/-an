---
name: Haute Gastronomie Poise
colors:
  surface: '#fcf9f4'
  surface-dim: '#dcdad5'
  surface-bright: '#fcf9f4'
  surface-container-lowest: '#ffffff'
  surface-container-low: '#f6f3ee'
  surface-container: '#f0ede9'
  surface-container-high: '#ebe8e3'
  surface-container-highest: '#e5e2dd'
  on-surface: '#1c1c19'
  on-surface-variant: '#4e4540'
  inverse-surface: '#31302d'
  inverse-on-surface: '#f3f0eb'
  outline: '#80756f'
  outline-variant: '#d2c4bd'
  surface-tint: '#6d5a50'
  primary: '#130803'
  on-primary: '#ffffff'
  primary-container: '#2c1e16'
  on-primary-container: '#9a8479'
  inverse-primary: '#dac2b5'
  secondary: '#765844'
  on-secondary: '#ffffff'
  secondary-container: '#fdd5bc'
  on-secondary-container: '#785b47'
  tertiary: '#100903'
  on-tertiary: '#ffffff'
  tertiary-container: '#282016'
  on-tertiary-container: '#948678'
  error: '#ba1a1a'
  on-error: '#ffffff'
  error-container: '#ffdad6'
  on-error-container: '#93000a'
  primary-fixed: '#f7ddd0'
  primary-fixed-dim: '#dac2b5'
  on-primary-fixed: '#261911'
  on-primary-fixed-variant: '#54433a'
  secondary-fixed: '#ffdcc6'
  secondary-fixed-dim: '#e5bfa6'
  on-secondary-fixed: '#2b1707'
  on-secondary-fixed-variant: '#5c412e'
  tertiary-fixed: '#f1e0cf'
  tertiary-fixed-dim: '#d4c4b4'
  on-tertiary-fixed: '#221a10'
  on-tertiary-fixed-variant: '#504539'
  background: '#fcf9f4'
  on-background: '#1c1c19'
  surface-variant: '#e5e2dd'
typography:
  display-lg:
    fontFamily: EB Garamond
    fontSize: 64px
    fontWeight: '400'
    lineHeight: 72px
    letterSpacing: -0.015em
  display-lg-mobile:
    fontFamily: EB Garamond
    fontSize: 40px
    fontWeight: '400'
    lineHeight: 46px
    letterSpacing: -0.01em
  headline-lg:
    fontFamily: EB Garamond
    fontSize: 44px
    fontWeight: '400'
    lineHeight: 52px
    letterSpacing: -0.01em
  headline-lg-mobile:
    fontFamily: EB Garamond
    fontSize: 30px
    fontWeight: '400'
    lineHeight: 36px
    letterSpacing: -0.005em
  headline-md:
    fontFamily: EB Garamond
    fontSize: 32px
    fontWeight: '400'
    lineHeight: 40px
    letterSpacing: -0.005em
  headline-sm:
    fontFamily: EB Garamond
    fontSize: 24px
    fontWeight: '500'
    lineHeight: 32px
  title-lg:
    fontFamily: DM Sans
    fontSize: 20px
    fontWeight: '500'
    lineHeight: 28px
  title-md:
    fontFamily: DM Sans
    fontSize: 16px
    fontWeight: '500'
    lineHeight: 24px
  body-lg:
    fontFamily: DM Sans
    fontSize: 18px
    fontWeight: '400'
    lineHeight: 28px
  body-md:
    fontFamily: DM Sans
    fontSize: 15px
    fontWeight: '400'
    lineHeight: 24px
  label-md:
    fontFamily: DM Sans
    fontSize: 12px
    fontWeight: '500'
    lineHeight: 16px
    letterSpacing: 0.14em
  label-sm:
    fontFamily: DM Sans
    fontSize: 11px
    fontWeight: '500'
    lineHeight: 14px
    letterSpacing: 0.16em
rounded:
  sm: 0.125rem
  DEFAULT: 0.25rem
  md: 0.375rem
  lg: 0.5rem
  xl: 0.75rem
  full: 9999px
spacing:
  gutter: 1.5rem
  margin: 2rem
  space-xs: 0.25rem
  space-sm: 0.5rem
  space-md: 1rem
  space-lg: 2rem
  space-xl: 4rem
---

## Brand & Style

The design system channels Parisian Haute Cuisine and the meticulous craft of L'Atelier, capturing the intimacy, poise, and heritage of a Michelin-starred dining salon. Built for discerning epicures and cultural travelers, it elevates every interaction into an architectural ritual: measured pacing, whispered luxury, and uncompromising precision.

Drawing inspiration from **Minimalism** infused with **Classical French Editorial Typography**:
- **Purity and Negative Space:** Whitespace acts as a canvas, allowing culinary artistry, sommelier selections, and artisanal textures to breathe with poise.
- **Warm Editorial Tactility:** The rich espresso and warm porcelain palette replaces clinical digital sterility with the tactile intimacy of bespoke menu parchment, fine linen, and polished walnut.
- **Architectural Harmony:** Poetic, high-contrast serif letterforms command editorial prestige, contrasted with whisper-quiet, modern grotesque sans-serif for functional legibility.

## Colors

The Atelier Espresso palette draws inspiration from dark roasted Arabica, vintage decanters, and hand-milled parchment, operating under a default **light** color mode.

- **Primary (`#130803`):** Deep roasted espresso anchoring typographic hierarchy, formal crests, and primary booking triggers.
- **Secondary (`#765844`):** Warm hazelnut and aged cognac tone, utilized for sommelier annotations, hover states, and warm borders.
- **Tertiary (`#100903`):** Ultra-dark roasted cacao providing tonal depth in structural backgrounds and elevated dark UI modes.
- **Neutral (`#FCF9F4`):** Warm porcelain cream that forms the baseline canvas, preventing screen glare and evoking textured cotton paper.
- **Functional Tiers:** Delicate container surfaces (`#F6F3EE` through `#E5E2DD`) distinguish multi-course tasting menus, wine cellar reserves, and reservation modules with natural tactility.

## Typography

The typographic hierarchy is anchored by classical French Garamond proportions, pairing **EB Garamond** with the contemporary, unencumbered legibility of **DM Sans**. This union bridges royal heritage with modern gastronomic clarity.

- **French Classical Headings:** EB Garamond drives all display and headline levels. Its refined x-height, delicate serifs, and high-contrast strokes reflect historical menu engraving and classical French literature.
- **Understated Subtitles and Body:** DM Sans handles dish provenance notes, allergen disclaimers, reservation steps, and interface copy, providing an unadorned, calming contrast.
- **Tracked Epigraphy:** `label-md` and `label-sm` are always rendered in uppercase with deliberate letter-spacing (`0.14em`–`0.16em`), embodying the quiet authority of grand cru vintage classifications and course identifiers (*AMUSE-BOUCHE*, *ENTRÉE*, *ACCORD METS ET VINS*).
- **Responsive Pacing:** Heading scale reduces gracefully on handheld displays via `display-lg-mobile` and `headline-lg-mobile` to maintain visual poise without awkward wrapping.

## Layout & Spacing

The layout model mimics the unhurried sequencing of a multi-course French service, balancing structured columns with breathing room.

- **Desktop (>= 1024px):** 12-column fluid grid with `2rem` (`32px`) margins and `1.5rem` (`24px`) gutters. Typical compositions favor asymmetrical pairings—such as a 5-column editorial culinary story set against a 7-column architectural photograph—separated by `space-xl` (`4rem`) vertical blocks.
- **Tablet (768px - 1023px):** 8-column layout with `1.5rem` margins and `1.25rem` gutters. Splits collapse to balanced 4-column groupings.
- **Mobile (< 768px):** 4-column layout with `1.25rem` margins and `1rem` gutters. Tasting courses, booking selectors, and imagery stack linearly with `space-lg` (`2rem`) vertical intervals.

## Elevation & Depth

Visual hierarchy rejects artificial volumetric shadows in favor of **tonal layering** and **low-contrast ghost hairlines**, reflecting the quiet restraint of fine table settings.

- **Hairline Framing:** Surface separation relies on `1px` borders rendered in `outline-variant` (`#D2C4BD`) at 40%–60% opacity.
- **Tonal Stepping:** Surfaces stack quietly from canvas (`#FCF9F4`) to subtle elevated tiers (`surface-container-low` `#F6F3EE` for menu cards; `surface-container-high` `#EBE8E3` for active modal layers).
- **Ambient Cellar Shadow:** Floating booking panels, table pickers, and sommelier overlays employ an ultra-diffused shadow tinted with deep espresso (`rgba(19, 8, 3, 0.05)`), featuring a `32px` blur and `8px` vertical translation.

## Shapes

The design system maintains a **Soft (`1`)** roundedness standard (`0.25rem` / `4px` base radius), delivering the crisp precision of French architectural trim while softening sharp edges to invite touch.

- **Base Radius (0.25rem / 4px):** Form fields, interactive buttons, modal dialogs, and culinary cards.
- **Subtle Badging:** Compact tags and vintage badges honor this subtle radius rather than aggressive rounded pills, maintaining tailored discipline.

## Components

- **Buttons:**
  - *Primary (Reservation CTA):* Solid `#130803` espresso fill with crisp `#FFFFFF` uppercase typography (`label-md`). Hover transitions smoothly to `#765844` with a subtle 200ms ease.
  - *Secondary (Wine Cellar / Menu Inquiries):* `#FCF9F4` ground with a crisp `1px` border in `#80756F` (50% opacity) and `#130803` text, shifting to `surface-container` (`#F0EDE9`) on hover.
  - *Text Action:* Refined espresso text with a stationary `1px` underline in `#D2C4BD` that deepens to `#765844` upon hover.
- **Chips & Dietary Indicators:** Minimalist `1px` outlined capsules in `outline-variant` (`#D2C4BD`) enclosing `label-sm` uppercase text (e.g., *CAVIAR IMPÉRIAL*, *ACCORD METS & VINS*, *SANS GLUTEN*).
- **Tasting Menu & Carte Lists:** Structured vertical stacks separated by delicate `1px` dividers (`#D2C4BD` at 40% opacity). Course names use `headline-sm` in EB Garamond; ingredients and provenance use `body-md` in DM Sans with right-aligned pricing in `label-md`.
- **Form Controls (Checkboxes & Radios):** Custom `1px` espresso-bordered squares (`4px` corner radius) or circles with unhurried transitions. Active states fill with `#130803` and display a warm cream check or pip.
- **Input Fields (Table Booking & Date Selection):** Underlined or fully enclosed minimalist inputs on `#FCF9F4` with floating `label-sm` labels. Focus states transition the border to solid `#130803` without harsh outer glow.
- **Cards (Chef's Table & Seasonal Menus):** Framed in `1px` `outline-variant` borders over `surface-container-lowest` or `surface-container-low`, padded with `space-lg` (`2rem`). Hero photography sits flush to the top with tailored `4px` top corners.
- **Sommelier Notes & Vignettes:** Inset quote blocks set in italicized `headline-sm` EB Garamond, flanked by a subtle left hairline in `#765844`.