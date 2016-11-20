namespace Logikfabrik.Overseer.Settings
{
    public interface IBuildProviderSettingsSerializer
    {
        /// <summary>
        /// Deserializes the specified settings.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <returns>The deserialized settings.</returns>
        BuildProviderSettings[] Deserialize(string settings);

        /// <summary>
        /// Serializes the specified settings.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <returns>The serialized settings.</returns>
        string Serialize(BuildProviderSettings[] settings);
    }
}