---
name: Atelier Espresso
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
    fontFamily: Playfair Display
    fontSize: 64px
    fontWeight: '400'
    lineHeight: 72px
    letterSpacing: -0.02em
  display-lg-mobile:
    fontFamily: Playfair Display
    fontSize: 40px
    fontWeight: '400'
    lineHeight: 46px
    letterSpacing: -0.015em
  headline-lg:
    fontFamily: Playfair Display
    fontSize: 44px
    fontWeight: '400'
    lineHeight: 52px
    letterSpacing: -0.01em
  headline-lg-mobile:
    fontFamily: Playfair Display
    fontSize: 30px
    fontWeight: '400'
    lineHeight: 36px
    letterSpacing: -0.01em
  headline-md:
    fontFamily: Playfair Display
    fontSize: 32px
    fontWeight: '400'
    lineHeight: 40px
    letterSpacing: -0.005em
  headline-sm:
    fontFamily: Playfair Display
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
    letterSpacing: 0.12em
  label-sm:
    fontFamily: DM Sans
    fontSize: 11px
    fontWeight: '500'
    lineHeight: 14px
    letterSpacing: 0.14em
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

This design system embodies quiet luxury and modern culinary artistry, crafted for a fine-dining restaurant website. It speaks to discerning diners, culinary enthusiasts, and hospitality guests seeking an experience defined by intimacy, provenance, and effortless sophistication. The interface should evoke the atmosphere of a candlelit dining room at dusk—warm, unhurried, and deeply intentional.

The visual language is rooted in **Minimalism** with an **Editorial** sensibility:
- **Generous Whitespace:** Layouts breathe, allowing photography of plated dishes, natural textures, and typography to command attention without visual clutter.
- **Restrained Palette:** Earthy espresso, warm brown, taupe, and cream create a grounded, tactile warmth that feels organic rather than sterile.
- **Typographic Poise:** High-contrast serif headlines bring literary elegance and heritage, balanced by clean, understated sans-serif body copy for effortless legibility.

## Colors

The palette draws from roasted coffee beans, aged oak, raw linen, and warm parchment. Defaulting to a **light** color mode, the warm cream canvas (`#FAF7F2`) replaces stark white to reduce glare and evoke fine stationery.

- **Primary (`#2C1E16`):** Deep espresso used for primary typography, primary action buttons, and key structural anchors.
- **Secondary (`#7A5C48`):** Rich warm brown reserved for secondary accents, active states, and subtle editorial highlights.
- **Tertiary (`#B8A99A`):** Muted taupe used for hairline borders, dividers, and subdued metadata.
- **Neutral (`#FAF7F2`):** Warm cream serving as the primary background canvas, paired with slightly deeper linen tones (`#F2ECE4`) for elevated cards and tasting menu sections.

## Typography

Typography pairs **Playfair Display** for editorial headings with **DM Sans** for body copy and interface labels. Playfair Display's high-contrast strokes and classical proportions lend an unmistakable fine-dining gravitas, while DM Sans provides a calm, geometric counterpoint.

- **Editorial Restraint:** Use regular (`400`) weights for large serif headlines to preserve delicacy; reserve medium (`500`) for smaller subheadings.
- **Tracked Labels:** `label-md` and `label-sm` should always be styled in uppercase with wide letter-spacing (`0.12em`–`0.14em`) for course titles, reservation prompts, and dietary tags.
- **Responsive Scaling:** Hero and section headlines step down gracefully via `display-lg-mobile` and `headline-lg-mobile` below `768px` to maintain balanced proportions on handheld screens.

## Layout & Spacing

The design system uses a **12-column fluid grid** with generous outer margins and deliberate negative space to mirror the pacing of a multi-course tasting menu.

- **Desktop (`>= 1024px`):** 12 columns with `2rem` (`32px`) outer margins and `1.5rem` (`24px`) gutters. Editorial sections frequently use asymmetric splits (e.g., 5 columns for narrative copy, 7 columns for full-bleed photography) separated by `space-xl` (`4rem`) vertical rhythm.
- **Tablet (`768px - 1023px`):** 8 columns with `1.5rem` margins. Asymmetric splits collapse into balanced 4/4 pairs or stacked editorial blocks.
- **Mobile (`< 768px`):** 4 columns with `1.25rem` margins and `1rem` gutters. All multi-column layouts stack vertically with `space-lg` (`2rem`) between content groups.

## Elevation & Depth

Hierarchy is conveyed through **low-contrast outlines** and **subtle, warm-tinted ambient shadows**, avoiding heavy drop shadows or stark contrast jumps.

- **Refined Borders:** Primary separation relies on `1px` hairline borders in muted taupe (`#B8A99A` at 35%–50% opacity) to frame tasting menus, reservation modules, and editorial cards.
- **Tonal Layers:** Interactive surfaces and secondary panels shift subtly from the `#FAF7F2` cream canvas to a warmer linen (`#F2ECE4`).
- **Ambient Shadows:** Floating elements (such as reservation drawers, date pickers, and sticky navigation) use an extra-diffused shadow tinted with deep espresso (`rgba(44, 30, 22, 0.06)`), with a `24px` blur radius and `8px` vertical offset.

## Shapes

A **Soft (`1`)** roundedness level (`0.25rem` / `4px` base radius) strikes a balance between architectural crispness and tactile warmth. Buttons, input fields, and cards feature subtle `4px` corners—softening the geometry just enough to feel welcoming without losing editorial precision.

## Components

- **Buttons:**
  - *Primary:* Solid espresso (`#2C1E16`) fill with warm cream (`#FAF7F2`) uppercase tracked label (`label-md`). Hover states transition smoothly to warm brown (`#7A5C48`).
  - *Secondary:* Transparent fill with a `1px` espresso border and espresso text, filling with `#F2ECE4` on hover.
  - *Text / Editorial Link:* Espresso text with a `1px` bottom hairline that expands or shifts to warm brown on hover.
- **Chips (Dietary & Course Tags):** Minimalist `1px` taupe-bordered pills (`#B8A99A` at 50% opacity) with `label-sm` uppercase text (e.g., *GF*, *Vegan*, *Wine Pairing*).
- **Lists (Tasting Menu & Wine List):** Clean vertical stacks separated by `1px` hairline dividers. Dish titles use `headline-sm` or `title-md`, paired with `body-md` descriptions and right-aligned pricing in `DM Sans`.
- **Checkboxes & Radio Buttons:** Custom `1px` taupe-bordered squares (`4px` radius) or circles. Selected states fill with espresso (`#2C1E16`) and display a cream checkmark or inner dot.
- **Input Fields (Reservations & Inquiries):** Bottom-bordered or `1px` full-bordered fields on a `#FAF7F2` background with `label-sm` floating or top-aligned labels. Focus states sharpen the border to solid espresso (`#2C1E16`).
- **Cards (Seasonal Dishes & Private Dining):** Framed with `1px` taupe borders or subtle linen (`#F2ECE4`) backgrounds, generous internal padding (`space-lg`), and `4px` rounded corners. Imagery sits flush or inset with `space-md` breathing room.
- **Editorial Dividers:** Thin `1px` horizontal rules in `#B8A99A` (30% opacity) with `space-xl` vertical clearance to pace long-form storytelling.