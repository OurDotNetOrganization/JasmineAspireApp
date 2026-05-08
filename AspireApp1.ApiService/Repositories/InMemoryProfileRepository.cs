using AspireApp1.ApiService.Contracts;

namespace AspireApp1.ApiService.Repositories;

public sealed class InMemoryProfileRepository : IProfileRepository
{
    private PublicProfileDto _profile;

    public InMemoryProfileRepository()
    {
        _profile = CreateSeedProfile();
    }

    public Task<PublicProfileDto> GetPublicAsync(CancellationToken cancellationToken = default)
        => Task.FromResult(_profile);

    public Task UpdateAsync(PublicProfileDto profile, CancellationToken cancellationToken = default)
    {
        _profile = profile;
        return Task.CompletedTask;
    }

    private static PublicProfileDto CreateSeedProfile() =>
        new(
            Kicker: "Marketing & communications · AI & sportstech",
            FullName: "Jasmine Dwidar",
            HeroParagraph:
            "I help deep-tech and sportstech brands turn complex innovation into clear stories, trusted partnerships, " +
            "and campaigns that scale — as Director of Marketing & Communications at Optomatica and Racemate.",
            Links: new ProfileLinksDto(
                LinkedIn: "https://www.linkedin.com/in/jasmine-dwidar/",
                Optomatica: "https://www.optomatica.com/",
                Racemate: "https://racemate.ai/"),
            ValueTitle: "What I help partners achieve",
            ValueIntro:
            "A hybrid of brand leadership and go-to-market storytelling: positioning that earns attention, " +
            "narratives that survive scrutiny, and launches that connect product truth to audience motivation.",
            ValueOutcomes:
            [
                "Clarity under complexity — translate AI and product depth into messaging stakeholders understand.",
                "Credible momentum — align brand, content, and comms so launches feel consistent across channels.",
                "Partnership-ready growth — support initiatives that pair innovation with ethics, community, and sport.",
            ],
            FocusAreas:
            [
                new FocusCardDto(
                    "AI communications",
                    "Positioning, narrative architecture, and stakeholder messaging for deep-tech products — from strategy decks to launch campaigns."),
                new FocusCardDto(
                    "Sportstech growth",
                    "Brand-building for running communities: training journeys, race partnerships, and product storytelling that matches athlete motivation."),
                new FocusCardDto(
                    "Partnerships & trust",
                    "Ethical, consistent comms across crises and milestones — aligning teams, agencies, and executives on what to say and why."),
            ],
            CurrentRoles:
            [
                new CurrentRoleDto(
                    Title: "Director of Marketing & Communications",
                    CompanyName: "Optomatica",
                    CompanyUrl: "https://www.optomatica.com/",
                    CompanySubtitle: " — AI-first consulting & engineering",
                    DateRange: "Sep 2022 — present",
                    Description: "Positioning, integrated campaigns, and stakeholder comms for AI-driven client work."),
                new CurrentRoleDto(
                    Title: "Director of Marketing & Communications",
                    CompanyName: "Racemate",
                    CompanyUrl: "https://racemate.ai/",
                    CompanySubtitle: " — AI-powered running & training companion",
                    DateRange: "Dec 2021 — present",
                    Description: "Brand growth, community storytelling, and partnerships across races and virtual journeys."),
            ],
            AboutBody:
            "My background blends conflict resolution and social development with senior marketing leadership. " +
            "I build programs that integrate AI with ethical practice: crisis readiness, brand architecture, " +
            "editorial direction, and stakeholder engagement — so teams ship narratives that are ambitious and accountable.",
            AboutMetaLine: "Based in Egypt · Marketing & communications experience 11+ years (per public LinkedIn profile)",
            HighlightsIntro: "Curated from Jasmine's public LinkedIn profile and activity — not an exhaustive CV.",
            HighlightBullets:
            [
                "Prior role: Head of Marketing, Optomatica (Sep 2018 — Oct 2022).",
                "Earlier path: Teacher (The International School of Choueifat); NGO program coordination; " +
                "researcher & media focal point with IOM (UN Migration), Cairo.",
                "Initiatives visible on LinkedIn: shared Racemate partnership and community posts " +
                "(e.g. Palestine Marathon collaboration, virtual running journeys, team events).",
            ],
            SelectedExperience:
            [
                new ExperienceLineDto("Head of Marketing", "Optomatica · Sep 2018 — Oct 2022"),
                new ExperienceLineDto("Teacher", "The International School of Choueifat · 2011 — 2013"),
                new ExperienceLineDto("Program Coordinator", "Alwan and Awtar NGO · 2011"),
                new ExperienceLineDto("Researcher & media focal point", "IOM (UN Migration) · Cairo · 2007"),
            ],
            TestimonialQuote:
            "Working with Jasmine is such a joy. I admire her integrity, creativity, and dedication to making an impact " +
            "that goes beyond just great marketing. She lifts the people around her, inspires our team, and I feel lucky " +
            "to call her a colleague and a friend.",
            TestimonialAttribution: "Sarah Rafea, as shown on Jasmine's public LinkedIn profile.",
            EducationPrimary: "Uppsala University — Uppsala, Sweden (2002 — 2006)",
            EducationFootnote:
            "Professional development includes digital marketing and social media programs (Windsor Educational Academy, Udemy), " +
            "as listed publicly on LinkedIn.",
            Skills:
            [
                "Strategic marketing",
                "Marketing communications",
                "Crisis management",
                "Brand development",
                "Stakeholder engagement",
                "AI and ethics messaging",
                "Content leadership",
                "Team building",
                "Conflict resolution",
            ],
            CtaTitle: "Let's explore a partnership",
            CtaBody: "AI launches, sportstech collaborations, media, and brand moments — start with a LinkedIn message or use the form below.",
            FooterNote: "This page is a demo built from publicly available information. Profile content may be served from the API."
        );
}
