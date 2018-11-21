using AdaptiveCards;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Office365DevBootCamp.Web.Services
{

    public class CardService
    {
        AdaptiveCard _AdaptiveCard = new AdaptiveCard();
        public string CreateTestCard()
        {
            _AdaptiveCard.Body.Add(new AdaptiveTextBlock()
            {
                Text = "Hello",
                Size = AdaptiveTextSize.ExtraLarge
            });

            _AdaptiveCard.Body.Add(new AdaptiveImage()
            {
                Url = new Uri("http://adaptivecards.io/content/cats/1.png")
            });
            //_AdaptiveCard.Actions.Add();
            // serialize the card to JSON
            string json = _AdaptiveCard.ToJson();
            return json;
        }
        public async Task<AdaptiveCard> GetTestCard(string CardURL)
        {
            AdaptiveCard _AdaptiveCard = new AdaptiveCard();
            try
            {
                // Get a JSON-serialized payload
                // Your app will probably get cards from somewhere else :)
                //var client = new HttpClient("http://adaptivecards.io/payloads/ActivityUpdate.json");
                var client = new HttpClient();
                var response = await client.GetAsync(CardURL);
                var json = await response.Content.ReadAsStringAsync();

                // Parse the JSON 
                AdaptiveCardParseResult result = AdaptiveCard.FromJson(json);

                // Get card from result
                _AdaptiveCard = result.Card;

                // Optional: check for any parse warnings
                // This includes things like unknown element "type"
                // or unknown properties on element
                IList<AdaptiveWarning> warnings = result.Warnings;
            }
            catch (AdaptiveSerializationException ex)
            {
                // Failed to deserialize card 
                // This occurs from malformed JSON
                // or schema violations like required properties missing 
            }
            return _AdaptiveCard;
        }
    }
}
