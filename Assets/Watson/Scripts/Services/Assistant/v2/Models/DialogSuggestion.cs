/**
* Copyright 2018 IBM Corp. All Rights Reserved.
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
*      http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*
*/

using FullSerializer;

namespace IBM.WatsonDeveloperCloud.Assistant.v2
{
    /// <summary>
    /// DialogSuggestion
    /// </summary>
    [fsObject]
    public class DialogSuggestion
    {
        /// <summary>
        /// The user-facing label for the disambiguation option. This label is taken from the **user_label** property of
        /// the corresponding dialog node.
        /// </summary>
        [fsProperty("label")]
        public string Label { get; set; }
        /// <summary>
        /// An object defining the message input to be sent to the assistant if the user selects the corresponding
        /// disambiguation option.
        /// </summary>
        [fsProperty("value")]
        public DialogSuggestionValue Value { get; set; }
        /// <summary>
        /// The dialog output that will be returned from the Watson Assistant service if the user selects the
        /// corresponding option.
        /// </summary>
        [fsProperty("output")]
        public object Output { get; set; }
    }

}
