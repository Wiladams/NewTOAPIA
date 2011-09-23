using System.Diagnostics;

public class Test_fatstring
{

//-----------------------------------------------------------------------------
// Test Bench
//-----------------------------------------------------------------------------
void main()
{
	char[] output = new byte[255];
	char[] output2 = new    byte[255];

	assert(fatfs_total_path_levels("C:\\folder\\file.zip") == 1);
	assert(fatfs_total_path_levels("C:\\file.zip") == 0);
	assert(fatfs_total_path_levels("C:\\folder\\folder2\\file.zip") == 2);
	assert(fatfs_total_path_levels("C:\\") == -1);
	assert(fatfs_total_path_levels("") == -1);
	assert(fatfs_total_path_levels("/dev/etc/file.zip") == 2);
	assert(fatfs_total_path_levels("/dev/file.zip") == 1);

	assert(fatfs_get_substring("C:\\folder\\file.zip", 0, output, sizeof(output)) == 0);
	assert(strcmp(output, "folder") == 0);

	assert(fatfs_get_substring("C:\\folder\\file.zip", 1, output, sizeof(output)) == 0);
	assert(strcmp(output, "file.zip") == 0);

	assert(fatfs_get_substring("/dev/etc/file.zip", 0, output, sizeof(output)) == 0);
	assert(strcmp(output, "dev") == 0);

	assert(fatfs_get_substring("/dev/etc/file.zip", 1, output, sizeof(output)) == 0);
	assert(strcmp(output, "etc") == 0);

	assert(fatfs_get_substring("/dev/etc/file.zip", 2, output, sizeof(output)) == 0);
	assert(strcmp(output, "file.zip") == 0);

	assert(fatfs_split_path("C:\\folder\\file.zip", output, sizeof(output), output2, sizeof(output2)) == 0);
	assert(strcmp(output, "C:\\folder") == 0);
	assert(strcmp(output2, "file.zip") == 0);

	assert(fatfs_split_path("C:\\file.zip", output, sizeof(output), output2, sizeof(output2)) == 0);
	assert(output[0] == 0);
	assert(strcmp(output2, "file.zip") == 0);

	assert(fatfs_split_path("/dev/etc/file.zip", output, sizeof(output), output2, sizeof(output2)) == 0);
	assert(strcmp(output, "/dev/etc") == 0);
	assert(strcmp(output2, "file.zip") == 0);

	assert(FileString_GetExtension("C:\\file.zip") == strlen("C:\\file"));
	assert(FileString_GetExtension("C:\\file.zip.ext") == strlen("C:\\file.zip"));
	assert(FileString_GetExtension("C:\\file.zip.") == strlen("C:\\file.zip"));

	assert(FileString_TrimLength("C:\\file.zip", strlen("C:\\file.zip")) == strlen("C:\\file.zip"));
	assert(FileString_TrimLength("C:\\file.zip   ", strlen("C:\\file.zip   ")) == strlen("C:\\file.zip"));
	assert(FileString_TrimLength("   ", strlen("   ")) == 0);

	assert(fatfs_compare_names("C:\\file.ext", "C:\\file.ext") == 1);
	assert(fatfs_compare_names("C:\\file2.ext", "C:\\file.ext") == 0);
	assert(fatfs_compare_names("C:\\file  .ext", "C:\\file.ext") == 1);
	assert(fatfs_compare_names("C:\\file  .ext", "C:\\file2.ext") == 0);
}
}